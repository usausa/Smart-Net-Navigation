namespace Example.WindowsApp.Modules.Stack
{
    using Smart.Navigation;
    using Smart.Windows.Input;

    public class Stack3ViewModel : AppViewModelBase
    {
        public AsyncCommand<int> Pop { get; }

        public Stack3ViewModel(INavigator navigator)
        {
            Pop = MakeAsyncCommand<int>(navigator.PopAsync);
        }
    }
}
