namespace Example.WindowsFormsApp.Modules.Wizard
{
    using System.Diagnostics.CodeAnalysis;

    using Smart.Navigation;
    using Smart.Navigation.Attributes;
    using Smart.Navigation.Plugins.Scope;

    [View(ViewId.WizardResult)]
    public partial class WizardResultView : AppViewBase
    {
        public override string Title => "Result";

        public override bool CanGoHome => true;

        [Scope]
        [AllowNull]
        public WizardContext Context { get; set; }

        public WizardResultView()
        {
            InitializeComponent();
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            Data1Label.Text = Context.Data1;
            Data2Label.Text = Context.Data2;
        }

        public override void OnGoHome()
        {
            Navigator.Forward(ViewId.Menu);
        }

        private void OnPrevButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(ViewId.WizardInput2);
        }

        private void OnNextButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(ViewId.Menu);
        }
    }
}
