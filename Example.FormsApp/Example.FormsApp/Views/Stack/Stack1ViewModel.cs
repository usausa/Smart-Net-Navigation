namespace Example.FormsApp.Views.Stack
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack1ViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> Forward { get; }

        public Stack1ViewModel()
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }
    }
}
