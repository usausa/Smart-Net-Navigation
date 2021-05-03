namespace Smart.Navigation.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        public virtual void OnCreate(IPluginContext context, object view, object target)
        {
        }

        public virtual void OnClose(IPluginContext context, object view, object target)
        {
        }

        public virtual void OnNavigatingFrom(IPluginContext context, object? view, object? target)
        {
        }

        public virtual void OnNavigatingTo(IPluginContext context, object view, object target)
        {
        }

        public virtual void OnNavigatedTo(IPluginContext context, object view, object target)
        {
        }
    }
}
