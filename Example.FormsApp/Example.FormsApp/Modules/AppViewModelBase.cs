namespace Example.FormsApp.Modules
{
    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class AppViewModelBase : ViewModelBase, INavigatorAware, IShellEventSupport
    {
        public INavigator Navigator { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
        }

        public virtual void ProcessGoHome()
        {
        }

        public virtual void ProcessFunction(FunctionKeys key)
        {
        }
    }
}
