namespace Example.WindowsApp.Views
{
    using System;
    using Smart.Navigation;
    using Smart.Windows.Input;

    public class MenuViewViewModel : AppViewModelBase
    {
        public AsyncCommand<Type> Forward { get; }

        public MenuViewViewModel(INavigator navigator)
        {
            Forward = MakeAsyncCommand<Type>(navigator.ForwardAsync);
        }
    }
}
