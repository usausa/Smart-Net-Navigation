namespace Smart.Navigation.Plugins;

public interface IPlugin
{
    void OnCreate(IPluginContext pluginContext, object view, object target);

    void OnClose(IPluginContext pluginContext, object view, object target);

    void OnNavigatingFrom(IPluginContext pluginContext, INavigationContext navigationContext, object? view, object? target);

    void OnNavigatingTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object target);

    void OnNavigatedTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object target);
}
