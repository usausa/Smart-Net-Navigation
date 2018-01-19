namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Functional;
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;
    using Smart.Navigation.Strategies;

    /// <summary>
    ///
    /// </summary>
    public sealed class Navigator : DisposableObject, INavigator
    {
        // ------------------------------------------------------------
        // Event
        // ------------------------------------------------------------

        public event EventHandler<ConfirmEventArgs> Confirm;

        public event EventHandler<NavigationEventArgs> Navigating;

        public event EventHandler<NavigationEventArgs> Navigated;

        public event EventHandler<EventArgs> Exited;

        // ------------------------------------------------------------
        // Member
        // ------------------------------------------------------------

        private static readonly NavigationParameter EmptyParameter = new NavigationParameter();

        private readonly ComponentContainer components;

        private readonly Dictionary<object, PageDescriptor> descriptors = new Dictionary<object, PageDescriptor>();

        private readonly List<PageStackInfo> pageStack = new List<PageStackInfo>();

        private readonly INavigationProvider provider;

        private readonly IFactory factory;

        private readonly IPlugin[] plugins;

        // ------------------------------------------------------------
        // Property
        // ------------------------------------------------------------

        private PageStackInfo CurrentStack => pageStack.Count > 0 ? pageStack[pageStack.Count - 1] : null;

        public int StackedCount => pageStack.Count;

        public object CurrentPageId => CurrentStack?.Descriptor.Id;

        public object CurrentPage => CurrentStack?.Page;

        public object CurrentTarget => CurrentStack?.Page.MapOrDefalut(x => provider.ResolveTarget(x));

        // ------------------------------------------------------------
        // Constructor
        // ------------------------------------------------------------

        public Navigator(INavigatorConfig config)
        {
            components = config.ResolveComponents();

            provider = components.Get<INavigationProvider>();
            factory = components.Get<IFactory>();
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
        // Registration
        // ------------------------------------------------------------

        public void Register(object id, Type type)
        {
            descriptors.Add(id, new PageDescriptor(id, type));
        }

        // ------------------------------------------------------------
        // Navigation
        // ------------------------------------------------------------

        public void Exit()
        {
            for (var i = pageStack.Count - 1; i >= 0; i--)
            {
                var page = pageStack[i].Page;
                provider.ClosePage(page);
            }

            pageStack.Clear();

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

            var navigationContext = new NavigationContext(CurrentPageId, result.ToId, result.Attribute, parameter ?? EmptyParameter);

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

            var navigationContext = new NavigationContext(CurrentPageId, result.ToId, result.Attribute, parameter ?? EmptyParameter);

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
            var pluginContext = new PluginContext();
            controller.PluginContext = pluginContext;

            var fromPage = CurrentPage;
            var fromTarget = CurrentTarget;

            var toPage = strategy.ResolveToPage(controller);
            var toTarget = provider.ResolveTarget(toPage);

            var args = new NavigationEventArgs(navigationContext, fromPage, fromTarget, toPage, toTarget);

            // Process from page
            if (fromPage != null)
            {
                (fromTarget as INavigationEventSupport)?.OnNavigatedFrom(navigationContext);

                foreach (var plugin in plugins)
                {
                    plugin.OnNavigatedFrom(pluginContext, fromPage, fromTarget);
                }
            }

            // Process navigating
            foreach (var plugin in plugins)
            {
                plugin.OnNavigatingTo(pluginContext, toPage, toTarget);
            }

            (toTarget as INavigationEventSupport)?.OnNavigatingTo(navigationContext);

            // End pre process
            Navigating?.Invoke(this, args);

            // Update stack
            strategy.UpdateStack(controller, toPage);

            // Process navigated
            foreach (var plugin in plugins)
            {
                plugin.OnNavigatedTo(pluginContext, toPage, toTarget);
            }

            (toTarget as INavigationEventSupport)?.OnNavigatedTo(navigationContext);

            // End post process
            Navigated?.Invoke(this, args);
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

            public IDictionary<object, PageDescriptor> Descriptors => navigator.descriptors;

            public List<PageStackInfo> PageStack => navigator.pageStack;

            public PluginContext PluginContext { private get; set; }

            public Controller(Navigator navigator)
            {
                this.navigator = navigator;
            }

            public object CreatePage(Type type)
            {
                var page = navigator.factory.Create(type);

                var target = navigator.provider.ResolveTarget(page);

                if (target is INavigatorAware aware)
                {
                    aware.Navigator = navigator;
                }

                foreach (var plugin in navigator.plugins)
                {
                    plugin.OnCreate(PluginContext, page, target);
                }

                return page;
            }

            public void OpenPage(object page)
            {
                navigator.provider.OpenPage(page);
            }

            public void ClosePage(object page)
            {
                var target = navigator.provider.ResolveTarget(page);

                foreach (var plugin in navigator.plugins)
                {
                    plugin.OnClose(PluginContext, page, target);
                }

                navigator.provider.ClosePage(page);
            }

            public void ActivePage(object page, object parameter)
            {
                navigator.provider.ActivePage(page, parameter);
            }

            public object DeactivePage(object page)
            {
                return navigator.provider.DeactivePage(page);
            }
        }
    }
}
