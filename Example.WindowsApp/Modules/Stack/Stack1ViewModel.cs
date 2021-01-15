namespace Example.WindowsApp.Modules.Stack
{
    using System;

    using Smart.Navigation;
    using Smart.Windows.Input;

    public class Stack1ViewModel : AppViewModelBase
    {
        public AsyncCommand<Type> Forward { get; }

        public AsyncCommand<Type> Push { get; }

        public Stack1ViewModel(INavigator navigator)
        {
            Forward = MakeAsyncCommand<Type>(navigator.ForwardAsync);
            Push = MakeAsyncCommand<Type>(navigator.PushAsync);
        }
    }
}
