namespace Smart.Navigation.Plugins;

public interface IPlugin
{
    void OnCreate(IPluginContext pluginContext, object view, object? target);

    void OnClose(IPluginContext pluginContext, object view, object? target);

    /// <summary>
    /// NavigationContext 構築直後、Confirm より前に呼び出される。
    /// 共通パラメータ（セッション、相関 ID、テナント等）を注入する用途で使用する。
    /// 複数プラグインが登録されている場合、AddPlugin の登録順で呼び出される。
    /// このフックは副作用を持たないこと（Confirm が false の場合、他のフックは呼ばれない）。
    /// </summary>
    void OnPrepareParameter(IPluginContext pluginContext, INavigationContext navigationContext, IMutableNavigationParameter parameter)
    {
        // 既存実装互換のため default interface method として no-op を提供
    }

    void OnNavigatingFrom(IPluginContext pluginContext, INavigationContext navigationContext, object? view, object? target);

    void OnNavigatingTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object? target);

    void OnNavigatedTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object? target);
}
