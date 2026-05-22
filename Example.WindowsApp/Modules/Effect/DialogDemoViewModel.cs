namespace Example.WindowsApp.Modules.Effect;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class DialogDemoViewModel : AppViewModelBase
{
    public string? AppliedEffect { get; private set; }

    public IObserveCommand Back { get; }

    public DialogDemoViewModel(INavigator navigator)
    {
        Back = MakeAsyncCommand(() =>
            navigator.ForwardAsync(typeof(EffectMenuView)));
    }

    public override void OnNavigatedTo(INavigationContext context)
    {
        AppliedEffect = context.Parameter.Effect ?? "(none)";
        RaisePropertyChanged(nameof(AppliedEffect));
    }
}
