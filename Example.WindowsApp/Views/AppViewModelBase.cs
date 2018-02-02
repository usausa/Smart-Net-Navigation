namespace Example.WindowsApp.Views
{
    using Smart.Navigation;
    using Smart.Windows.ViewModels;

    public class AppViewModelBase : ViewModelBase, INavigatorAware
    {
        public INavigator Navigator { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
        }
    }
}
