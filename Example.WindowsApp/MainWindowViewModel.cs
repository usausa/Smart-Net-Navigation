namespace Example.WindowsApp
{
    using Smart.Navigation;
    using Smart.Windows.ViewModels;

    public class MainWindowViewModel : ViewModelBase
    {
        public INavigator Navigator { get; }

        public MainWindowViewModel(INavigator navigator)
        {
            Navigator = navigator;
        }
    }
}
