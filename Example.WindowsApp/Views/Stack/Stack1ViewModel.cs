namespace Example.WindowsApp.Views.Stack
{
    using System;

    using Smart.Navigation;
    using Smart.Windows.Input;

    public class Stack1ViewModel : AppViewModelBase
    {
        public AsyncCommand<Type> Forward { get; }

        public Stack1ViewModel()
        {
            Forward = MakeAsyncCommand<Type>(x => Navigator.ForwardAsync(x));
        }
    }
}
