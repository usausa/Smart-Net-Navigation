namespace Example.WindowsApp.Modules.Stack
{
    using System;

    using Smart.Navigation;
    using Smart.Windows.Input;

    public class Stack2ViewModel : AppViewModelBase
    {
        public AsyncCommand<int> Pop { get; }

        public AsyncCommand<Type> Push { get; }

        public Stack2ViewModel(INavigator navigator)
        {
            Pop = MakeAsyncCommand<int>(navigator.PopAsync);
            Push = MakeAsyncCommand<Type>(navigator.PushAsync);
        }
    }
}
