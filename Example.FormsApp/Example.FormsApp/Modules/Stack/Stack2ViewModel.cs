namespace Example.FormsApp.Modules.Stack
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack2ViewModel : AppViewModelBase, INotifySupportAsync<FunctionKeys>
    {
        public AsyncCommand<int> Pop { get; }

        public AsyncCommand<ViewId> Push { get; }

        public Stack2ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Pop = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
            Push = MakeAsyncCommand<ViewId>(x => Navigator.PushAsync(x));
        }

        public Task NavigatorNotifyAsync(FunctionKeys parameter)
        {
            switch (parameter)
            {
                case FunctionKeys.Function1:
                    return Navigator.PopAsync();
                case FunctionKeys.Function4:
                    return Navigator.PushAsync(ViewId.Stack3);
                default:
                    return Task.CompletedTask;
            }
        }
    }
}
