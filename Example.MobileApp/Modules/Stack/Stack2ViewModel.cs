namespace Example.MobileApp.Modules.Stack;

using System.Windows.Input;

using Smart.Navigation;

public class Stack2ViewModel : AppViewModelBase
{
    public ICommand PopCommand { get; }

    public ICommand PushCommand { get; }

    public Stack2ViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
        PopCommand = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
        PushCommand = MakeAsyncCommand<ViewId>(x => Navigator.PushAsync(x));
    }

    protected override Task OnNotifyFunction1Async()
    {
        return Navigator.PopAsync();
    }

    protected override Task OnNotifyFunction4Async()
    {
        return Navigator.PushAsync(ViewId.Stack3);
    }
}
