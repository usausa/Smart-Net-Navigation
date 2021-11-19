namespace Smart.Navigation.Plugins;

public abstract class PluginBase : IPlugin
{
    public virtual void OnCreate(IPluginContext pluginContext, object view, object target)
    {
    }

    public virtual void OnClose(IPluginContext pluginContext, object view, object target)
    {
    }

    public virtual void OnNavigatingFrom(IPluginContext pluginContext, INavigationContext navigationContext, object? view, object? target)
    {
    }

    public virtual void OnNavigatingTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object target)
    {
    }

    public virtual void OnNavigatedTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object target)
    {
    }
}
