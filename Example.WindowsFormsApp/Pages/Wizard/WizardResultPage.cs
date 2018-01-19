namespace Example.WindowsFormsApp.Pages.Wizard
{
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    [Page(PageId.WizardResult)]
    public partial class WizardResultPage : AppPageBase
    {
        public override string Title => "Result";

        public override bool CanBack => true;

        [Scope]
        public WizardContext Context { get; set; }

        public WizardResultPage()
        {
            InitializeComponent();
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            Data1Label.Text = Context.Data1;
            Data2Label.Text = Context.Data2;
        }

        public override void OnBack()
        {
            Navigator.Forward(PageId.WizardInput2);
        }

        private void OnPrevButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.WizardInput2);
        }

        private void OnNextButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.Menu);
        }
    }
}
