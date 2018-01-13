namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.ComponentModel;
    using Smart.Functional;
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;
    using Smart.Navigation.Strategies;

    /// <summary>
    ///
    /// </summary>
    public class Navigator : DisposableObject, INavigator
    {
        // ------------------------------------------------------------
        // Event
        // ------------------------------------------------------------

        public event EventHandler<ConfirmEventArgs> Confirm;

        public event EventHandler<NavigationEventArgs> NavigatedFrom;

        public event EventHandler<NavigationEventArgs> NavigatingTo;

        public event EventHandler<NavigationEventArgs> NavigatedTo;

        public event EventHandler<EventArgs> Exited;

        // ------------------------------------------------------------
        // Member
        // ------------------------------------------------------------

        private static readonly NavigationParameter EmptyParameter = new NavigationParameter();

        private readonly ComponentContainer components;

        private readonly Dictionary<object, PageDescriptor> descriptors = new Dictionary<object, PageDescriptor>();

        private readonly PageStackManager stackManager = new PageStackManager();

        private readonly INavigationProvider provider;

        private readonly IFactory factory;

        private readonly IPlugin[] plugins;

        // ------------------------------------------------------------
        // Property
        // ------------------------------------------------------------

        public int StackedCount => stackManager.Stacked.Count;

        public object CurrentPageId => stackManager.CurrentPageId;

        public object CurrentPage => stackManager.CurrentPage;

        public object CurrentTarget => stackManager.CurrentPage.MapOrDefalut(x => provider.ResolveTarget(x));

        // ------------------------------------------------------------
        // Constructor
        // ------------------------------------------------------------

        public Navigator()
            : this(new NavigatorConfig())
        {
        }

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
            for (var i = stackManager.Stacked.Count - 1; i >= 0; i--)
            {
                var page = stackManager.Stacked[i].Page;
                var target = provider.ResolveTarget(page);

                provider.ClosePage(page);

                (page as IDisposable)?.Dispose();
                if (page != target)
                {
                    (target as IDisposable)?.Dispose();
                }
            }

            stackManager.Stacked.Clear();

            Exited?.Invoke(this, EventArgs.Empty);
        }

        public bool Navigate(INavigationStrategy strategy, INavigationParameter parameter)
        {
            var controller = new Controller(this);
            var result = strategy.Initialize(controller);
            var navigationContext = new NavigationContext(CurrentPageId, result.ToId, result.Attribute, parameter ?? EmptyParameter);

            if (!ConfirmNavigation(navigationContext))
            {
                return false;
            }

            var args = new NavigationEventArgs(navigationContext);
            var pluginContext = new PluginContext();
            controller.PluginContext = pluginContext;

            var fromPage = CurrentPage;
            var fromTarget = CurrentTarget;

            // Process from page
            if (fromPage != null)
            {
                (fromTarget as INavigationEventSupport)?.OnNavigatedFrom(navigationContext);

                foreach (var plugin in plugins)
                {
                    plugin.OnNavigatedFrom(pluginContext, fromPage, fromTarget);
                }

                NavigatedFrom?.Invoke(this, args);
            }

            // Create to page
            var toPage = strategy.ResolveToPage(controller);
            var toTarget = provider.ResolveTarget(toPage);

            // Process navigating
            NavigatingTo?.Invoke(this, args);

            foreach (var plugin in plugins)
            {
                plugin.OnNavigatingTo(pluginContext, toPage, toTarget);
            }

            (toTarget as INavigationEventSupport)?.OnNavigatingTo(navigationContext);

            // Update stack
            strategy.UpdateStack(controller, toPage);

            // Process navigated
            NavigatedTo?.Invoke(this, args);

            foreach (var plugin in plugins)
            {
                plugin.OnNavigatedTo(pluginContext, toPage, toTarget);
            }

            (toTarget as INavigationEventSupport)?.OnNavigatedTo(navigationContext);

            return true;
        }

        // TODO async?

        // ------------------------------------------------------------
        // Helper
        // ------------------------------------------------------------

        private bool ConfirmNavigation(NavigationContext context)
        {
            var page = CurrentPage;
            if (page != null)
            {
                var target = provider.ResolveTarget(page);
                if (target is IConfirmRequest confirm)
                {
                    var cancel = confirm.NavigationConfirm(context);
                    if (cancel)
                    {
                        return false;
                    }
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

        // ------------------------------------------------------------
        // Controller
        // ------------------------------------------------------------

        private class Controller : INavigationController
        {
            private readonly Navigator navigator;

            public IDictionary<object, PageDescriptor> Descriptors => navigator.descriptors;

            public PageStackManager StackManager => navigator.stackManager;

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

                if (page != target)
                {
                    (target as IDisposable)?.Dispose();
                }
            }

            public void ActivaPage(object page, object parameter)
            {
                navigator.provider.ActivePage(page, parameter);
            }

            public object DeactivePage(object page)
            {
                return navigator.provider.DectivePage(page);
            }
        }
    }
}
