namespace Smart.Navigation
{
    using System;

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

        public NavigationController(
            INavigator navigator,
            IFactory factory,
            INavigationProvider provider,
            IPlugin[] plugins,
            INavigationContext navigationContext,
            IPluginContext pluginContext)
        {
            this.navigator = navigator;
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

            (page as IDisposable)?.Dispose();
            if (page != target)
            {
                (target as IDisposable)?.Dispose();
            }
        }

            public void ActivaPage(PageStack stack)
        {
            throw new NotImplementedException();
        }

        public void DeactivePage(PageStack stack)
        {
            throw new NotImplementedException();
        }
    }
}
