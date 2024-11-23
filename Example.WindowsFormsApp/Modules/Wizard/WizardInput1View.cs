namespace Example.WindowsFormsApp.Modules.Wizard;

using System.ComponentModel;

using Smart.Navigation;
using Smart.Navigation.Attributes;
using Smart.Navigation.Plugins.Scope;

[View(ViewId.WizardInput1)]
public sealed partial class WizardInput1View : AppViewBase
{
    public override string Title => "Input1";

    public override bool CanGoHome => true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Scope]
    public WizardContext Context { get; set; } = default!;

    public WizardInput1View()
    {
        InitializeComponent();
    }

    public override void OnNavigatingTo(INavigationContext context)
    {
        Data1Text.Text = Context.Data1;
    }

    public override void OnGoHome()
    {
        Navigator.Forward(ViewId.Menu);
    }

    private void OnPrevButtonClick(object sender, EventArgs e)
    {
        Navigator.Forward(ViewId.Menu);
    }

    private void OnNextButtonClick(object sender, EventArgs e)
    {
        Context.Data1 = Data1Text.Text;

        Navigator.Forward(ViewId.WizardInput2);
    }
}
