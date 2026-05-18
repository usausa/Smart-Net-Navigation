namespace Example.WindowsApp.Modules.Animation;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class AnimationDemoViewModel : AppViewModelBase
{
    public string? LastAnimationKind { get; private set; }

    public IObserveCommand Back { get; }

    public AnimationDemoViewModel(INavigator navigator)
    {
        Back = MakeAsyncCommand(() =>
            navigator.ForwardAsync(typeof(AnimationMenuView), new NavigationParameter().WithBackAnimation()));
    }

    public override void OnNavigatedTo(INavigationContext context)
    {
        LastAnimationKind = context.Parameter.AnimationKind ?? "(none)";
        RaisePropertyChanged(nameof(LastAnimationKind));
    }
}
