namespace Example.WindowsFormsApp.Pages.Wizard
{
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    [Page(PageId.WizardInput2)]
    public partial class WizardInput2Page : AppPageBase
    {
        public override string Title { get; } = "Input2";

        public override bool CanBack => true;

        [Scope]
        public WizardContext Context { get; set; }

        public WizardInput2Page()
        {
            InitializeComponent();
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            Data2Text.Text = Context.Data2;
        }

        public override void OnBack()
        {
            Navigator.Forward(PageId.WizardInput1);
        }

        private void OnPrevButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.WizardInput1);
        }

        private void OnNextButtonClick(object sender, System.EventArgs e)
        {
            Context.Data2 = Data2Text.Text;

            Navigator.Forward(PageId.WizardResult);
        }
    }
}
