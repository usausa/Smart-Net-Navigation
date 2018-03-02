namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Functional;
    using Smart.Navigation.Components;
    using Smart.Navigation.Mappers;
    using Smart.Navigation.Plugins;
    using Smart.Navigation.Strategies;

    /// <summary>
    ///
    /// </summary>
    public sealed class Navigator : DisposableObject, INavigator, INavigatorComponentSource
    {
        // ------------------------------------------------------------
        // Event
        // ------------------------------------------------------------

        public event EventHandler<ConfirmEventArgs> Confirm;

        public event EventHandler<NavigationEventArgs> Navigating;

        public event EventHandler<NavigationEventArgs> Navigated;

        public event EventHandler<EventArgs> Exited;

        public event EventHandler<EventArgs> ExecutingChanged;

        // ------------------------------------------------------------
        // Member
        // ------------------------------------------------------------

        private static readonly NavigationParameter EmptyParameter = new NavigationParameter();

        private readonly ComponentContainer components;

        private readonly List<ViewStackInfo> viewStack = new List<ViewStackInfo>();

        private readonly INavigationProvider provider;

        private readonly IViewMapper viewMapper;

        private readonly IActivator activator;

        private readonly IPlugin[] plugins;

        // ------------------------------------------------------------
        // Property
        // ------------------------------------------------------------

        ComponentContainer INavigatorComponentSource.Components => components;

        private ViewStackInfo CurrentStack => viewStack.Count > 0 ? viewStack[viewStack.Count - 1] : null;

        public int StackedCount => viewStack.Count;

        public object CurrentViewId => CurrentStack?.Descriptor.Id;

        public object CurrentView => CurrentStack?.View;

        public object CurrentTarget => CurrentStack?.View.MapOrDefalut(x => provider.ResolveTarget(x));

        public bool Executing { get; private set; }

        // ------------------------------------------------------------
        // Constructor
        // ------------------------------------------------------------

        public Navigator(INavigatorConfig config)
        {
            components = config.ResolveComponents();

            provider = components.Get<INavigationProvider>();
            viewMapper = components.Get<IViewMapper>();
            activator = components.Get<IActivator>();
            plugins = components.GetAll<IPlugin>().ToArray();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // ------------------------------------------------------------
        // Navigation
        // ------------------------------------------------------------

        public void Exit()
        {
            for (var i = viewStack.Count - 1; i >= 0; i--)
            {
                var view = viewStack[i].View;
                provider.CloseView(view);
            }

            viewStack.Clear();

            viewMapper.CurrentUpdated(null);

            Exited?.Invoke(this, EventArgs.Empty);
        }

        bool INavigator.Navigate(INavigationStrategy strategy, INavigationParameter parameter)
        {
            var controller = new Controller(this);
            var result = strategy.Initialize(controller);
            if (result == null)
            {
                return false;
            }

            var navigationContext = new NavigationContext(CurrentViewId, result.ToId, result.Attribute, parameter ?? EmptyParameter);

            if (!ConfirmNavigation(navigationContext))
            {
                return false;
            }

            NavigateCore(strategy, navigationContext, controller);

            return true;
        }

        async Task<bool> INavigator.NavigateAsync(INavigationStrategy strategy, INavigationParameter parameter)
        {
            var controller = new Controller(this);
            var result = strategy.Initialize(controller);
            if (result == null)
            {
                return false;
            }

            var navigationContext = new NavigationContext(CurrentViewId, result.ToId, result.Attribute, parameter ?? EmptyParameter);

            var confirmResult = await ConfirmNavigationAsync(navigationContext);
            if (!confirmResult)
            {
                return false;
            }

            NavigateCore(strategy, navigationContext, controller);

            return true;
        }

        private void NavigateCore(INavigationStrategy strategy, INavigationContext navigationContext, Controller controller)
        {
            try
            {
                Executing = true;
                ExecutingChanged?.Invoke(this, EventArgs.Empty);

                var pluginContext = new PluginContext();
                controller.PluginContext = pluginContext;

                var fromView = CurrentView;
                var fromTarget = CurrentTarget;

                var toView = strategy.ResolveToView(controller);
                var toTarget = provider.ResolveTarget(toView);

                var args = new NavigationEventArgs(navigationContext, fromView, fromTarget, toView, toTarget);

                // Process from view
                if (fromView != null)
                {
                    (fromTarget as INavigationEventSupport)?.OnNavigatedFrom(navigationContext);

                    foreach (var plugin in plugins)
                    {
                        plugin.OnNavigatedFrom(pluginContext, fromView, fromTarget);
                    }
                }

                // Process navigating
                foreach (var plugin in plugins)
                {
                    plugin.OnNavigatingTo(pluginContext, toView, toTarget);
                }

                (toTarget as INavigationEventSupport)?.OnNavigatingTo(navigationContext);

                // End pre process
                Navigating?.Invoke(this, args);

                // Update stack
                strategy.UpdateStack(controller, toView);

                // Update view mapper
                viewMapper.CurrentUpdated(CurrentViewId);

                // Process navigated
                foreach (var plugin in plugins)
                {
                    plugin.OnNavigatedTo(pluginContext, toView, toTarget);
                }

                (toTarget as INavigationEventSupport)?.OnNavigatedTo(navigationContext);

                // End post process
                Navigated?.Invoke(this, args);
            }
            finally
            {
                Executing = false;
                ExecutingChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // ------------------------------------------------------------
        // Helper
        // ------------------------------------------------------------

        private bool ConfirmNavigation(NavigationContext context)
        {
            if (CurrentTarget is IConfirmRequest confirm)
            {
                var canNavigate = confirm.CanNavigate(context);
                if (!canNavigate)
                {
                    return false;
                }
            }

            var handler = Confirm;
            if (handler != null)
            {
                var args = new ConfirmEventArgs(context);
                handler(this, args);
                if (args.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> ConfirmNavigationAsync(NavigationContext context)
        {
            if (CurrentTarget is IConfirmRequestAsync confirm)
            {
                var canNavigate = await confirm.CanNavigateAsync(context);
                if (!canNavigate)
                {
                    return false;
                }
            }

            return ConfirmNavigation(context);
        }

        // ------------------------------------------------------------
        // Controller
        // ------------------------------------------------------------

        private sealed class Controller : INavigationController
        {
            private readonly Navigator navigator;

            public IViewMapper ViewMapper => navigator.viewMapper;

            public List<ViewStackInfo> ViewStack => navigator.viewStack;

            public PluginContext PluginContext { private get; set; }

            public Controller(Navigator navigator)
            {
                this.navigator = navigator;
            }

            public object CreateView(Type type)
            {
                var view = navigator.activator.Resolve(type);

                var target = navigator.provider.ResolveTarget(view);

                if (target is INavigatorAware aware)
                {
                    aware.Navigator = navigator;
                }

                foreach (var plugin in navigator.plugins)
                {
                    plugin.OnCreate(PluginContext, view, target);
                }

                return view;
            }

            public void OpenView(object view)
            {
                navigator.provider.OpenView(view);
            }

            public void CloseView(object view)
            {
                var target = navigator.provider.ResolveTarget(view);

                foreach (var plugin in navigator.plugins)
                {
                    plugin.OnClose(PluginContext, view, target);
                }

                navigator.provider.CloseView(view);
            }

            public void ActiveView(object view, object parameter)
            {
                navigator.provider.ActiveView(view, parameter);
            }

            public object DeactiveView(object view)
            {
                return navigator.provider.DeactiveView(view);
            }
        }
    }
}
