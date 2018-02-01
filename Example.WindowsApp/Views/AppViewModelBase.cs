namespace Example.WindowsApp.Views
{
    using Smart.Navigation;
    using Smart.Windows.ViewModels;

    public class AppViewModelBase : ViewModelBase, INavigatorAware
    {
        public INavigator Navigator { get; set; }
    }
}
