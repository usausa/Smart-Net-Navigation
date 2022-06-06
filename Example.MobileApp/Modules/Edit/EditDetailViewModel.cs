namespace Example.MobileApp.Modules.Edit;

using Example.MobileApp.Models;
using Example.MobileApp.Services;

using Smart.ComponentModel;
using Smart.Navigation;
using Smart.Resolver.Attributes;

public class EditDetailViewModel : AppViewModelBase
{
    private DataEntity entity = default!;

    [Inject]
    public DataService DataService { get; set; } = default!;

    public NotificationValue<bool> Update { get; } = new();

    public NotificationValue<string> Name { get; } = new();

    public EditDetailViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
    }

    public override void OnNavigatingTo(INavigationContext context)
    {
        Update.Value = Equals(context.ToId, ViewId.EditDetailUpdate);
        if (Update.Value)
        {
            entity = context.Parameter.GetValue<DataEntity>();
            Name.Value = entity.Name;
        }
    }

    protected override Task OnNotifyFunction1Async()
    {
        return Navigator.ForwardAsync(ViewId.EditList);
    }

    protected override Task OnNotifyFunction4Async()
    {
        if (Update.Value)
        {
            entity.Name = Name.Value;
            DataService.UpdateData(entity);
        }
        else
        {
            DataService.InsertData(Name.Value);
        }

        return Navigator.ForwardAsync(ViewId.EditList);
    }
}
