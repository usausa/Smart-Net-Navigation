namespace Example.WindowsFormsApp.Pages
{
    using System.Windows.Forms;

    using Smart.Navigation;

    public partial class AppPageBase : UserControl, IApplicationPage, INavigatorAware
    {
        public virtual string Title { get; } = string.Empty;

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
    }
}
