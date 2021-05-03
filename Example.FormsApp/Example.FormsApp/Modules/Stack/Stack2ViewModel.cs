namespace Example.FormsApp.Modules.Stack
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack2ViewModel : AppViewModelBase
    {
        public AsyncCommand<int> PopCommand { get; }

        public AsyncCommand<ViewId> PushCommand { get; }

        public Stack2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            PopCommand = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
            PushCommand = MakeAsyncCommand<ViewId>(x => Navigator.PushAsync(x));
        }

        protected override Task OnNotifyFunction1Async()
        {
            return Navigator.PopAsync();
        }

        protected override Task OnNotifyFunction4Async()
        {
            return Navigator.PushAsync(ViewId.Stack3);
        }
    }
}
