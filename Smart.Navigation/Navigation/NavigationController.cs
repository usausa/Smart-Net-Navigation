namespace Smart.Navigation
{
    using Smart.Navigation.Plugins;

    public class NavigationController : INavigationController
    {
        public INavigationProvider Provider { get; }

        public IPluginPipeline Pipeline { get; }

        public IPluginContext PluginContext { get; }

        public INavigationContext NavigationContext { get; }

        public NavigationController(
            INavigationProvider provider,
            IPluginPipeline pipeline,
            IPluginContext pluginContext,
            INavigationContext navigationContext)
        {
            Provider = provider;
            Pipeline = pipeline;
            PluginContext = pluginContext;
            NavigationContext = navigationContext;
        }
    }
}
