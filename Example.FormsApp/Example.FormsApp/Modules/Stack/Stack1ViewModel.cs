namespace Example.FormsApp.Modules.Stack
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack1ViewModel : AppViewModelBase, INotifySupportAsync<FunctionKeys>
    {
        public AsyncCommand<ViewId> Forward { get; }

        public AsyncCommand<ViewId> Push { get; }

        public Stack1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
            Push = MakeAsyncCommand<ViewId>(x => Navigator.PushAsync(x));
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
