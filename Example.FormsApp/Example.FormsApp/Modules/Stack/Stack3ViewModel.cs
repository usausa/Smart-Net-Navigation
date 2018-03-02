namespace Example.FormsApp.Modules.Stack
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack3ViewModel : AppViewModelBase, INotifySupportAsync<FunctionKeys>
    {
        public AsyncCommand<int> PopCommand { get; }

        public Stack3ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            PopCommand = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
        }

        public Task NavigatorNotifyAsync(FunctionKeys parameter)
        {
            switch (parameter)
            {
                case FunctionKeys.Function1:
                    return Navigator.PopAsync();
                case FunctionKeys.Function2:
                    return Navigator.PopAsync(2);
                default:
                    return Task.CompletedTask;
            }
        }
    }
}
