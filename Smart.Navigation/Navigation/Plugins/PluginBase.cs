namespace Smart.Navigation.Plugins
{
    public class PluginBase : IPlugin
    {
        public virtual void OnCreate(IPluginContext context, object page, object target)
        {
        }

        public virtual void OnClose(IPluginContext context, object page, object target)
        {
        }

        public virtual void OnNavigatedFrom(IPluginContext context, object page, object target)
        {
        }

        public virtual void OnNavigatedTo(IPluginContext context, object page, object target)
        {
        }
    }
}
