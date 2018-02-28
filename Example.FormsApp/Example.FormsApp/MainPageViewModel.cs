namespace Example.FormsApp
{
    using System.Threading.Tasks;

    using Example.FormsApp.Views;

    using Smart.Forms.Input;
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

        private bool function1Enabled;

        private bool function2Enabled;

        private bool function3Enabled;

        private bool function4Enabled;

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

        public bool Function1Enabled
        {
            get => function1Enabled;
            set => SetProperty(ref function1Enabled, value);
        }

        public bool Function2Enabled
        {
            get => function2Enabled;
            set => SetProperty(ref function2Enabled, value);
        }

        public bool Function3Enabled
        {
            get => function3Enabled;
            set => SetProperty(ref function3Enabled, value);
        }

        public bool Function4Enabled
        {
            get => function4Enabled;
            set => SetProperty(ref function4Enabled, value);
        }

        public ApplicationState ApplicationState { get; }

        public INavigator Navigator { get; }

        public AsyncCommand GoHome { get; }

        public MainPageViewModel(ApplicationState applicationState, INavigator navigator)
            : base(applicationState)
        {
            ApplicationState = applicationState;
            Navigator = navigator;

            // TODO
            GoHome = MakeAsyncCommand(() => Task.Delay(3000));
        }
    }
}
