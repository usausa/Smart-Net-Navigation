namespace Example.FormsApp.Modules.Stack
{
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class Stack3ViewModel : AppViewModelBase
    {
        public AsyncCommand<int> PopCommand { get; }

        public Stack3ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            PopCommand = MakeAsyncCommand<int>(x => Navigator.PopAsync(x));
        }

        protected override Task OnNotifyFunction1Async()
        {
            return Navigator.PopAsync();
        }

        protected override Task OnNotifyFunction2Async()
        {
            return Navigator.PopAsync(2);
        }
    }
}
