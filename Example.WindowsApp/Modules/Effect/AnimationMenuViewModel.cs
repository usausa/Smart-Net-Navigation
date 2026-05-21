namespace Example.WindowsApp.Modules.Effect;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class AnimationMenuViewModel : AppViewModelBase
{
    public IObserveCommand Navigate { get; }

    public AnimationMenuViewModel(INavigator navigator)
    {
        Navigate = MakeAsyncCommand<string>(kind =>
            navigator.ForwardAsync(typeof(AnimationDemoView), new NavigationParameter().WithEffect(kind)));
    }
}
