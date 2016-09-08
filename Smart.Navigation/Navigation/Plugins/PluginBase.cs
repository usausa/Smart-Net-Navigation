namespace Smart.Navigation.Plugins
{
    public class PluginBase : IPlugin
    {
        public virtual void OnCreate(PluginContext context, object page, object target)
        {
        }

        public virtual void OnDispose(PluginContext context, object page, object target)
        {
        }

        public virtual void OnNavigatedFrom(PluginContext context, object page, object target)
        {
        }

        public virtual void OnNavigatedTo(PluginContext context, object page, object target)
        {
        }
    }
}
