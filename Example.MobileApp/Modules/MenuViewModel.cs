namespace Example.MobileApp.Modules;

using System.Windows.Input;

using Smart.Navigation;

public class MenuViewModel : AppViewModelBase
{
    public ICommand ForwardCommand { get; }

    public MenuViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
        ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
    }
}
