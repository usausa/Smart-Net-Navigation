namespace Example.WindowsFormsApp.Modules.Edit;

using Example.WindowsFormsApp.Models;
using Example.WindowsFormsApp.Services;

using Smart.Navigation;
using Smart.Navigation.Attributes;
using Smart.Resolver.Attributes;

[View(ViewId.EditDetailNew)]
[View(ViewId.EditDetailUpdate)]
public sealed partial class DataDetailView : AppViewBase
{
    private bool update;

    private DataEntity entity = default!;

    public override string Title => update ? "Data New" : "Data Edit";

    public override bool CanGoHome => true;

    [Inject]
    public DataService DataService { get; set; } = default!;

    public DataDetailView()
    {
        InitializeComponent();
    }

    public override void OnNavigatingTo(INavigationContext context)
    {
        update = Equals(context.ToId, ViewId.EditDetailUpdate);
        if (update)
        {
            entity = context.Parameter.GetValue<DataEntity>();
            NameText.Text = entity.Name;
        }
    }

    public override void OnGoHome()
    {
        Navigator.Forward(ViewId.Menu);
    }

    private void OnPrevButtonClick(object sender, EventArgs e)
    {
        Navigator.Forward(ViewId.EditList);
    }

    private void OnUpdateButtonClick(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(NameText.Text))
        {
            NameText.Focus();
            return;
        }

        if (update)
        {
            entity.Name = NameText.Text;
            DataService.UpdateData(entity);
        }
        else
        {
            DataService.InsertData(NameText.Text);
        }

        Navigator.Forward(ViewId.EditList);
    }
}
