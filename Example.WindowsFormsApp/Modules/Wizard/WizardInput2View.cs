namespace Example.WindowsFormsApp.Modules.Wizard;

using Smart.Navigation;
using Smart.Navigation.Attributes;
using Smart.Navigation.Plugins.Scope;

[View(ViewId.WizardInput2)]
public sealed partial class WizardInput2View : AppViewBase
{
    public override string Title => "Input2";

    public override bool CanGoHome => true;

    [Scope]
    public WizardContext Context { get; set; } = default!;

    public WizardInput2View()
    {
        InitializeComponent();
    }

    public override void OnNavigatingTo(INavigationContext context)
    {
        Data2Text.Text = Context.Data2;
    }

    public override void OnGoHome()
    {
        Navigator.Forward(ViewId.Menu);
    }

    private void OnPrevButtonClick(object sender, System.EventArgs e)
    {
        Navigator.Forward(ViewId.WizardInput1);
    }

    private void OnNextButtonClick(object sender, System.EventArgs e)
    {
        Context.Data2 = Data2Text.Text;

        Navigator.Forward(ViewId.WizardResult);
    }
}
