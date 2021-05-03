namespace Example.FormsApp.Modules
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Example.FormsApp.Shell;

    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class AppViewModelBase : ViewModelBase, INavigatorAware, INavigationEventSupport, INotifySupportAsync<ShellEvent>
    {
        [AllowNull]
        public INavigator Navigator { get; set; }

        protected ApplicationState ApplicationState { get; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
        }

        protected AppViewModelBase(ApplicationState applicationState)
            : base(applicationState)
        {
            ApplicationState = applicationState;
        }

        public virtual void OnNavigatingFrom(INavigationContext context)
        {
        }

        public virtual void OnNavigatingTo(INavigationContext context)
        {
        }

        public virtual void OnNavigatedTo(INavigationContext context)
        {
        }

        public Task NavigatorNotifyAsync(ShellEvent parameter)
        {
            return parameter switch
            {
                ShellEvent.Function1 => OnNotifyFunction1Async(),
                ShellEvent.Function2 => OnNotifyFunction2Async(),
                ShellEvent.Function3 => OnNotifyFunction3Async(),
                ShellEvent.Function4 => OnNotifyFunction4Async(),
                _ => Task.CompletedTask
            };
        }

        protected virtual Task OnNotifyFunction1Async()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNotifyFunction2Async()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNotifyFunction3Async()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNotifyFunction4Async()
        {
            return Task.CompletedTask;
        }
    }
}
