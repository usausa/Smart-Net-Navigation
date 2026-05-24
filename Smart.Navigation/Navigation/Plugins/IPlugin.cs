namespace Smart.Navigation.Plugins;

public interface IPlugin
{
    void OnPrepareParameter(IPluginContext pluginContext, INavigationContext navigationContext, IMutableNavigationParameter parameter)
    {
        // TODO 既存実装互換のため default interface method として no-op を提供
    }

    void OnCreate(IPluginContext pluginContext, object view, object? target);

    void OnClose(IPluginContext pluginContext, object view, object? target);

    void OnNavigatingFrom(IPluginContext pluginContext, INavigationContext navigationContext, object? view, object? target);

    void OnNavigatingTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object? target);

    void OnNavigatedTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object? target);
}
