namespace Example.WindowsApp.Modules;

using Smart.Navigation;
using Smart.Windows.Input;

public sealed class MenuViewModel : AppViewModelBase
{
    public AsyncCommand<Type> Forward { get; }

    public MenuViewModel(INavigator navigator)
    {
        Forward = MakeAsyncCommand<Type>(navigator.ForwardAsync);
    }
}
