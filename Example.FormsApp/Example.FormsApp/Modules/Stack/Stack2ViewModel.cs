namespace Example.FormsApp.Modules.Stack
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack2ViewModel : AppViewModelBase
    {
        public AsyncCommand<int> Pop { get; }

        public AsyncCommand<ViewId> Push { get; }

        public Stack2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Pop = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
            Push = MakeAsyncCommand<ViewId>(x => Navigator.PushAsync(x));
        }
    }
}
