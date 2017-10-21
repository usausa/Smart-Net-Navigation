namespace Smart.Navigation
{
    using System;

    using Smart.ComponentModel;
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;

    public class NavigationController : INavigationController
    {
        private readonly INavigator navigator;

        public IComponentContainer Components { get; }

        public INavigationProvider Provider { get; }

        public IPluginPipeline Pipeline { get; }

        public IPluginContext PluginContext { get; }

        public INavigationContext NavigationContext { get; }

        public PageStackManager StackManager { get; }

        public NavigationController(
            IComponentContainer components,
            IPluginContext pluginContext,
            INavigationContext navigationContext,
            INavigator navigator,
            PageStackManager stackManager)
        {
            Components = components;
            Provider = components.Get<INavigationProvider>();
            Pipeline = components.Get<IPluginPipeline>();
            PluginContext = pluginContext;
            NavigationContext = navigationContext;
            this.navigator = navigator;
            StackManager = stackManager;
        }

        // ------------------------------------------------------------
        // Lifecycle
        // ------------------------------------------------------------

        public object CreatePage(Type type)
        {
            var page = Components.Get<IActivator>().Create(type);

            Provider.OpenPage(page);

            var target = Provider.ResolveTarget(page);

            var aware = target as INavigatorAware;
            if (aware != null)
            {
                aware.Navigator = navigator;
            }

            Pipeline?.OnCreate(PluginContext, page, target);

            return page;
        }

        public void ClosePage(object page)
        {
            var target = Provider.ResolveTarget(page);

            Pipeline?.OnClose(PluginContext, page, target);

            Provider.ClosePage(page);

            (page as IDisposable)?.Dispose();
            if (page != target)
            {
                (target as IDisposable)?.Dispose();
            }
        }

        public void ActivaPage()
        {
            var stack = StackManager.CurrentStack;

            Provider.ActivePage(stack.Page, stack.RestoreParameter);
        }

        public void DeactivePage()
        {
            var stack = StackManager.CurrentStack;

            stack.RestoreParameter = Provider.DectivePage(stack.Page);
        }

        // ------------------------------------------------------------
        // Navigation
        // ------------------------------------------------------------

        public void ProcessNavigatedFrom()
        {
            var page = StackManager.CurrentPage;
            if (page == null)
            {
                return;
            }

            var target = Provider.ResolveTarget(page);

            (target as INavigationEventSupport)?.OnNavigatedFrom(NavigationContext);

            Pipeline?.OnNavigatedFrom(PluginContext, page, target);
        }

        public void ProcessNavigatedTo()
        {
            var page = StackManager.CurrentPage;

            var target = Provider.ResolveTarget(page);

            Pipeline?.OnNavigatedTo(PluginContext, page, target);

            (target as INavigationEventSupport)?.OnNavigatedTo(NavigationContext);
        }
    }
}
