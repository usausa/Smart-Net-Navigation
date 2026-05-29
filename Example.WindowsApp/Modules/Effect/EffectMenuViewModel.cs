namespace Example.WindowsApp.Modules.Effect;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class EffectMenuViewModel : AppViewModelBase
{
    public IObserveCommand Navigate { get; }

    public IObserveCommand NavigateDialog { get; }

    public EffectMenuViewModel(INavigator navigator)
    {
        Navigate = MakeAsyncCommand<string>(x => navigator.ForwardAsync(typeof(EffectDemoView), new NavigationParameter().WithEffect(x)));
        NavigateDialog = MakeAsyncCommand(() => navigator.ForwardAsync(typeof(EffectDialogView)));
    }
}
