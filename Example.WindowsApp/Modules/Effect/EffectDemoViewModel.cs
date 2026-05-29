namespace Example.WindowsApp.Modules.Effect;

using Smart.Mvvm;
using Smart.Navigation;
using Smart.Windows.Input;

public sealed partial class EffectDemoViewModel : AppViewModelBase
{
    [ObservableProperty]
    public partial string? LastEffect { get; private set; }

    public IObserveCommand Back { get; }

    public EffectDemoViewModel(INavigator navigator)
    {
        Back = MakeAsyncCommand(() => navigator.ForwardAsync(typeof(EffectMenuView), new NavigationParameter().WithBackEffect()));
    }

    public override void OnNavigatedTo(INavigationContext context)
    {
        LastEffect = context.Parameter.Effect ?? "(none)";
    }
}
