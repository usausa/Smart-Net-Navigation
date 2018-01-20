namespace Smart.Navigation.Plugins
{
    public interface IPlugin
    {
        void OnCreate(IPluginContext context, object view, object target);

        void OnClose(IPluginContext context, object view, object target);

        void OnNavigatedFrom(IPluginContext context, object view, object target);

        void OnNavigatingTo(IPluginContext context, object view, object target);

        void OnNavigatedTo(IPluginContext context, object view, object target);
    }
}
