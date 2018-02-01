namespace Example.WindowsApp.Views
{
    using System;
    using Smart.Navigation;
    using Smart.Windows.Input;

    using Smart.Windows.ViewModels;

    public class MenuViewViewModel : AppViewModelBase
    {
        public AsyncCommand<Type> Forward { get; }

        public MenuViewViewModel()
        {
            Forward = MakeAsyncCommand<Type>(x => Navigator.ForwardAsync(x));
        }
    }
}
