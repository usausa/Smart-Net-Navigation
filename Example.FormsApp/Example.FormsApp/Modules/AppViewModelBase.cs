namespace Example.FormsApp.Modules
{
    using System.Threading.Tasks;

    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class AppViewModelBase : ViewModelBase, INavigatorAware, IShellEventSupport
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

        public virtual Task GoHomeAsync()
        {
            return Task.CompletedTask;
        }

        // TODO Function
    }
}
