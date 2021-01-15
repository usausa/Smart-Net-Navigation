namespace Example.WindowsApp.Modules
{
    using System;
    using Smart.Navigation;
    using Smart.Windows.Input;

    public class MenuViewModel : AppViewModelBase
    {
        public AsyncCommand<Type> Forward { get; }

        public MenuViewModel(INavigator navigator)
        {
            Forward = MakeAsyncCommand<Type>(navigator.ForwardAsync);
        }
    }
}
