namespace Example.WindowsFormsApp.Pages.Wizard
{
    using Smart.Navigation;

    [Page(PageId.WizardInput1)]
    public partial class WizardInput1Page : AppPageBase
    {
        public override bool CanBack => true;

        public WizardInput1Page()
        {
            InitializeComponent();
        }

        public override void OnBack()
        {
            Navigator.Forward(PageId.Menu);
        }
    }
}
