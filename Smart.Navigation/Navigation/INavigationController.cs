namespace Smart.Navigation
{
    using Smart.Navigation.Plugins;

    public interface INavigationController
    {
        INavigationProvider Provider { get; }

        IPluginPipeline Pipeline { get; }

        IPluginContext PluginContext { get; }

        INavigationContext NavigationContext { get; }
    }
}
