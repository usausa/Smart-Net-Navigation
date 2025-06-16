namespace Example.WindowsApp.Modules.Stack;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class Stack3ViewModel : AppViewModelBase
{
    public IObserveCommand Pop { get; }

    public Stack3ViewModel(INavigator navigator)
    {
        Pop = MakeAsyncCommand<int>(navigator.PopAsync);
    }
}
