namespace Example.WindowsApp.Modules.Effect;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class EffectDemoViewModel : AppViewModelBase
{
    public string? LastEffectKind { get; private set; }

    public IObserveCommand Back { get; }

    public EffectDemoViewModel(INavigator navigator)
    {
        Back = MakeAsyncCommand(() =>
            navigator.ForwardAsync(typeof(EffectMenuView), new NavigationParameter().WithBackEffect()));
    }

    public override void OnNavigatedTo(INavigationContext context)
    {
        LastEffectKind = context.Parameter.EffectKind ?? "(none)";
        RaisePropertyChanged(nameof(LastEffectKind));
    }
}
