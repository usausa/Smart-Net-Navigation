namespace Example.FormsApp.Modules.Stack
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack1ViewModel : AppViewModelBase, INotifySupportAsync<FunctionKeys>
    {
        public AsyncCommand<ViewId> ForwardCommand { get; }

        public AsyncCommand<ViewId> PushCommand { get; }

        public Stack1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
            PushCommand = MakeAsyncCommand<ViewId>(x => Navigator.PushAsync(x));
        }

        public Task NavigatorNotifyAsync(FunctionKeys parameter)
        {
            switch (parameter)
            {
                case FunctionKeys.Function1:
                    return Navigator.ForwardAsync(ViewId.Menu);
                case FunctionKeys.Function4:
                    return Navigator.PushAsync(ViewId.Stack2);
                default:
                    return Task.CompletedTask;
            }
        }
    }
}
