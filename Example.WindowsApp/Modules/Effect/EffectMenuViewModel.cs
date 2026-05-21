namespace Example.WindowsApp.Modules.Effect;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class EffectMenuViewModel : AppViewModelBase
{
    public IObserveCommand Navigate { get; }

    public EffectMenuViewModel(INavigator navigator)
    {
        Navigate = MakeAsyncCommand<string>(kind =>
            navigator.ForwardAsync(typeof(EffectDemoView), new NavigationParameter().WithEffect(kind)));
    }
}
