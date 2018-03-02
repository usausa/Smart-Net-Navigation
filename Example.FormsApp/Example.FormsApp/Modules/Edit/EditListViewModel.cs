namespace Example.FormsApp.Modules.Edit
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Example.FormsApp.Models;
    using Example.FormsApp.Services;

    using Smart.Collections.Generic;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Resolver.Attributes;

    public class EditListViewModel : AppViewModelBase, INavigationEventSupport, INotifySupportAsync<FunctionKeys>
    {
        [Inject]
        public DataService DataService { get; set; }

        public ObservableCollection<DataEntity> Items { get; } = new ObservableCollection<DataEntity>();

        public AsyncCommand<DataEntity> SelectCommand { get; }

        public EditListViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            SelectCommand = MakeAsyncCommand<DataEntity>(x =>
                Navigator.ForwardAsync(ViewId.EditDetailUpdate, new NavigationParameter().SetValue(x)));
        }

        public void OnNavigatedFrom(INavigationContext context)
        {
        }

        public void OnNavigatingTo(INavigationContext context)
        {
        }

        public void OnNavigatedTo(INavigationContext context)
        {
            Items.AddRange(DataService.QueryDataList());
        }

        public Task NavigatorNotifyAsync(FunctionKeys parameter)
        {
            switch (parameter)
            {
                case FunctionKeys.Function1:
                    return Navigator.ForwardAsync(ViewId.Menu);
                case FunctionKeys.Function4:
                    return Navigator.ForwardAsync(ViewId.EditDetailNew);
                default:
                    return Task.CompletedTask;
            }
        }
    }
}
