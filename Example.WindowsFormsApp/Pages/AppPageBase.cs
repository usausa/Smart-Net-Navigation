namespace Example.WindowsFormsApp.Pages
{
    using System.Windows.Forms;

    using Smart.Navigation;

    public partial class AppPageBase : UserControl, IApplicationPage, INavigatorAware, INavigationEventSupport
    {
        public virtual string Title { get; } = string.Empty;

        public virtual bool CanBack { get; } = false;

        public IFunctionControl FunctionControl { get; set; }

        public INavigator Navigator { get; set; }

        public AppPageBase()
        {
            InitializeComponent();
        }

        public virtual void OnFunctionKey(Keys key)
        {
        }

        public virtual void OnBack()
        {
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
