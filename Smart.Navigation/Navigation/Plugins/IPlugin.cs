namespace Smart.Navigation.Plugins
{
    public interface IPlugin
    {
        void OnPreProcess(IPluginContext context);

        void OnNavigatedFrom(IPluginContext context, object page, object target);

        void OnClose(IPluginContext context, object page, object target);

        void OnCreate(IPluginContext context, object page, object target);

        void OnNavigatedTo(IPluginContext context, object page, object target);

        void OnPostProcess(IPluginContext context);
    }
}
