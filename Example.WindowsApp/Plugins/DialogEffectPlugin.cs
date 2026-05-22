namespace Example.WindowsApp.Plugins;

using Example.WindowsApp.Animation;
using Example.WindowsApp.Views;

using Smart.Navigation;
using Smart.Navigation.Plugins;

/// <summary>
/// ダイアログ画面への遷移・遷移元に自動的に Dialog エフェクトを設定するプラグイン。
/// IDialogView を実装している View が遷移先または遷移元の場合、
/// 呼び出し元で Effect を明示しなくても Dialog アニメーションが再生される。
/// すでに Effect が設定されている場合は上書きしない。
/// </summary>
internal sealed class DialogEffectPlugin : PluginBase
{
    private static readonly Type DialogViewType = typeof(IDialogView);

    public override void OnPrepareParameter(
        IPluginContext pluginContext,
        INavigationContext navigationContext,
        IMutableNavigationParameter parameter)
    {
        // すでに Effect が設定されている場合は何もしない
        if (parameter.Effect is not null)
        {
            return;
        }

        var toType = navigationContext.ToId as Type;
        var fromType = navigationContext.FromId as Type;

        var toIsDialog = toType is not null && DialogViewType.IsAssignableFrom(toType);
        var fromIsDialog = fromType is not null && DialogViewType.IsAssignableFrom(fromType);

        if (toIsDialog)
        {
            // ダイアログ画面へ遷移 → オープンエフェクト
            parameter.WithEffect(ExampleEffect.DialogOpen);
        }
        else if (fromIsDialog)
        {
            // ダイアログ画面から遷移 → クローズエフェクト
            parameter.WithEffect(ExampleEffect.DialogClose);
        }
    }
}
