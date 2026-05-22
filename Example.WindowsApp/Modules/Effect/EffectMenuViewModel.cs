namespace Example.WindowsApp.Modules.Effect;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class EffectMenuViewModel : AppViewModelBase
{
    public IObserveCommand Navigate { get; }

    public IObserveCommand NavigateDialog { get; }

    public EffectMenuViewModel(INavigator navigator)
    {
        Navigate = MakeAsyncCommand<string>(kind =>
            navigator.ForwardAsync(typeof(EffectDemoView), new NavigationParameter().WithEffect(kind)));

        // Effect 指定なし — DialogEffectPlugin が自動で Dialog エフェクトを設定する
        NavigateDialog = MakeAsyncCommand(() =>
            navigator.ForwardAsync(typeof(DialogDemoView)));
    }
}
