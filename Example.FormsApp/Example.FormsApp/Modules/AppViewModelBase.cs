namespace Example.FormsApp.Modules
{
    using System.Threading.Tasks;

    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class AppViewModelBase : ViewModelBase, INavigatorAware, INavigationEventSupport, INotifySupportAsync<FunctionKeys>
    {
        public INavigator Navigator { get; set; }

        protected ApplicationState ApplicationState { get; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
        }

        public AppViewModelBase(ApplicationState applicationState)
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

        public Task NavigatorNotifyAsync(FunctionKeys parameter)
        {
            switch (parameter)
            {
                case FunctionKeys.Function1:
                    return OnNotifyFunction1Async();
                case FunctionKeys.Function2:
                    return OnNotifyFunction2Async();
                case FunctionKeys.Function3:
                    return OnNotifyFunction3Async();
                case FunctionKeys.Function4:
                    return OnNotifyFunction4Async();
                default:
                    return Task.CompletedTask;
            }
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
