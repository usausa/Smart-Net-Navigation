namespace Example.WindowsFormsApp.Views
{
    using System.Windows.Forms;

    using Smart.Navigation;

    public partial class AppViewBase : UserControl, IApplicationView, INavigatorAware, IConfirmRequest, INavigationEventSupport
    {
        public virtual string Title => string.Empty;

        public virtual bool CanGoHome => false;

        public virtual FunctionKey[] FunctionKeys => null;

        public INavigator Navigator { get; set; }

        public AppViewBase()
        {
            InitializeComponent();
        }

        public virtual void OnFunctionKey(Keys key)
        {
        }

        public virtual void OnGoHome()
        {
        }

        public virtual bool CanNavigate(INavigationContext context)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(INavigationContext context)
        {
        }

        public virtual void OnNavigatingTo(INavigationContext context)
        {
        }

        public virtual void OnNavigatedTo(INavigationContext context)
        {
        }
    }
}
