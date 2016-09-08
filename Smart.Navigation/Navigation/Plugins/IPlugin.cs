namespace Smart.Navigation.Plugins
{
    public interface IPlugin
    {
        void OnCreate(PluginContext context, object page, object target);

        void OnDispose(PluginContext context, object page, object target);

        void OnNavigatedFrom(PluginContext context, object page, object target);

        void OnNavigatedTo(PluginContext context, object page, object target);
    }
}
