namespace Example.FormsApp
{
    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class MainPageViewModel : ViewModelBase
    {
        public INavigator Navigator { get; }

        public MainPageViewModel(INavigator navigator)
        {
            System.Diagnostics.Debug.WriteLine("************************");

            Navigator = navigator;
        }
    }
}
