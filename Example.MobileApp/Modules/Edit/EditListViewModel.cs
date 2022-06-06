namespace Example.MobileApp.Modules.Edit;

using System.Collections.ObjectModel;
using System.Windows.Input;

using Example.MobileApp.Models;
using Example.MobileApp.Services;

using Smart.Collections.Generic;
using Smart.Navigation;
using Smart.Resolver.Attributes;

public class EditListViewModel : AppViewModelBase
{
    [Inject]
    public DataService DataService { get; set; } = default!;

    public ObservableCollection<DataEntity> Items { get; } = new();

    public ICommand SelectCommand { get; }

    public EditListViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
        SelectCommand = MakeAsyncCommand<DataEntity>(x =>
            Navigator.ForwardAsync(ViewId.EditDetailUpdate, new NavigationParameter().SetValue(x)));
    }

    public override void OnNavigatedTo(INavigationContext context)
    {
        Items.AddRange(DataService.QueryDataList());
    }

    protected override Task OnNotifyFunction1Async()
    {
        return Navigator.ForwardAsync(ViewId.Menu);
    }

    protected override Task OnNotifyFunction4Async()
    {
        return Navigator.ForwardAsync(ViewId.EditDetailNew);
    }
}
