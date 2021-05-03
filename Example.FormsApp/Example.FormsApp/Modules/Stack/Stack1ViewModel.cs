namespace Example.FormsApp.Modules.Stack
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack1ViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> ForwardCommand { get; }

        public AsyncCommand<ViewId> PushCommand { get; }

        public Stack1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
            PushCommand = MakeAsyncCommand<ViewId>(x => Navigator.PushAsync(x));
        }

        protected override Task OnNotifyFunction1Async()
        {
            return Navigator.ForwardAsync(ViewId.Menu);
        }

        protected override Task OnNotifyFunction4Async()
        {
            return Navigator.PushAsync(ViewId.Stack2);
        }
    }
}
