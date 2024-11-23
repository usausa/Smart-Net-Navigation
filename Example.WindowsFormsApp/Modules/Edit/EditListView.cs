namespace Example.WindowsFormsApp.Modules.Edit;

using System.ComponentModel;

using Example.WindowsFormsApp.Models;
using Example.WindowsFormsApp.Services;

using Smart.Navigation;
using Smart.Navigation.Attributes;
using Smart.Resolver.Attributes;

[View(ViewId.EditList)]
public sealed partial class DataListView : AppViewBase
{
    public override string Title => "Data List";

    public override bool CanGoHome => true;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Inject]
    public DataService DataService { get; set; } = default!;

    public DataListView()
    {
        InitializeComponent();
    }

    public override void OnNavigatingTo(INavigationContext context)
    {
        DataListBox.DataSource = DataService.QueryDataList().ToList();
    }

    public override void OnGoHome()
    {
        Navigator.Forward(ViewId.Menu);
    }

    private void OnNewButtonClick(object sender, EventArgs e)
    {
        Navigator.Forward(ViewId.EditDetailNew);
    }

    private void OnEditButtonClick(object sender, EventArgs e)
    {
        var parameter = new NavigationParameter();
        parameter.SetValue((DataEntity)DataListBox.SelectedItem!);
        Navigator.Forward(ViewId.EditDetailUpdate, parameter);
    }
}
