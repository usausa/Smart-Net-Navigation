namespace Example.WindowsApp.Modules.Stack;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class Stack1ViewModel : AppViewModelBase
{
    public IObserveCommand Forward { get; }

    public IObserveCommand Push { get; }

    public Stack1ViewModel(INavigator navigator)
    {
        Forward = MakeAsyncCommand<Type>(navigator.ForwardAsync);
        Push = MakeAsyncCommand<Type>(navigator.PushAsync);
    }
}
