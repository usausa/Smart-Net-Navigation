namespace Example.WindowsFormsApp.Pages.Wizard
{
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    [Page(PageId.WizardInput1)]
    public partial class WizardInput1Page : AppPageBase
    {
        public override string Title => "Input1";

        public override bool CanGoHome => true;

        [Scope]
        public WizardContext Context { get; set; }

        public WizardInput1Page()
        {
            InitializeComponent();
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            Data1Text.Text = Context.Data1;
        }

        public override void OnGoHome()
        {
            Navigator.Forward(PageId.Menu);
        }

        private void OnPrevButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.Menu);
        }

        private void OnNextButtonClick(object sender, System.EventArgs e)
        {
            Context.Data1 = Data1Text.Text;

            Navigator.Forward(PageId.WizardInput2);
        }
    }
}
