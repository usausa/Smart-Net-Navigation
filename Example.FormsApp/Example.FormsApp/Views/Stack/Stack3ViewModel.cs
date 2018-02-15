namespace Example.FormsApp.Views.Stack
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack3ViewModel : AppViewModelBase
    {
        public AsyncCommand<int> Pop { get; }

        public Stack3ViewModel()
        {
            Pop = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
        }
    }
}
