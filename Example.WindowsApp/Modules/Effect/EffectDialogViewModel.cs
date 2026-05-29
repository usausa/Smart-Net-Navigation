namespace Example.WindowsApp.Modules.Effect;

using Smart.Mvvm;
using Smart.Navigation;
using Smart.Windows.Input;

public sealed partial class EffectDialogViewModel : AppViewModelBase
{
    [ObservableProperty]
    public partial string? AppliedEffect { get; private set; }

    public IObserveCommand Back { get; }

    public EffectDialogViewModel(INavigator navigator)
    {
        Back = MakeAsyncCommand(() => navigator.ForwardAsync(typeof(EffectMenuView)));
    }

    public override void OnNavigatedTo(INavigationContext context)
    {
        AppliedEffect = context.Parameter.Effect ?? "(none)";
    }
}
