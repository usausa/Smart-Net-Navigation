namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;

    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;

    public class NavigationController : INavigationController
    {
        private readonly INavigator navigator;

        private readonly IFactory factory;

        private readonly INavigationProvider provider;

        private readonly IPlugin[] plugins;

        private readonly INavigationContext navigationContext;

        private readonly IPluginContext pluginContext;

        public IDictionary<object, PageDescriptor> Descriptors { get; }

        public PageStackManager StackManager { get; }

        public NavigationController(
            INavigator navigator,
            IDictionary<object, PageDescriptor> descriptors,
            PageStackManager stackManager,
            IFactory factory,
            INavigationProvider provider,
            IPlugin[] plugins,
            INavigationContext navigationContext,
            IPluginContext pluginContext)
        {
            this.navigator = navigator;
            Descriptors = descriptors;
            StackManager = stackManager;
            this.factory = factory;
            this.provider = provider;
            this.plugins = plugins;
            this.navigationContext = navigationContext;
            this.pluginContext = pluginContext;
        }

        public object CreatePage(Type type)
        {
            var page = factory.Create(type);

            provider.OpenPage(page);

            var target = provider.ResolveTarget(page);

            if (target is INavigatorAware aware)
            {
                aware.Navigator = navigator;
            }

            foreach (var plugin in plugins)
            {
                plugin.OnCreate(pluginContext, page, target);
            }

            return page;
        }

        public void ClosePage(object page)
        {
            var target = provider.ResolveTarget(page);

            foreach (var plugin in plugins)
            {
                plugin.OnClose(pluginContext, page, target);
            }

            provider.ClosePage(page);

            if (page != target)
            {
                (target as IDisposable)?.Dispose();
            }
        }

            public void ActivaPage(object page)
        {
            throw new NotImplementedException();
        }

        public void DeactivePage(object page)
        {
            throw new NotImplementedException();
        }
    }
}
