namespace Example.FormsApp
{
    using Example.FormsApp.Views;

    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class MainPageViewModel : ViewModelBase, IShellControl
    {
        private string title;

        private bool canGoHome;

        private string function1Text;

        private string function2Text;

        private string function3Text;

        private string function4Text;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public bool CanGoHome
        {
            get => canGoHome;
            set => SetProperty(ref canGoHome, value);
        }

        public string Function1Text
        {
            get => function1Text;
            set => SetProperty(ref function1Text, value);
        }

        public string Function2Text
        {
            get => function2Text;
            set => SetProperty(ref function2Text, value);
        }

        public string Function3Text
        {
            get => function3Text;
            set => SetProperty(ref function3Text, value);
        }

        public string Function4Text
        {
            get => function4Text;
            set => SetProperty(ref function4Text, value);
        }

        public INavigator Navigator { get; }

        public MainPageViewModel(INavigator navigator)
        {
            Navigator = navigator;
        }
    }
}
