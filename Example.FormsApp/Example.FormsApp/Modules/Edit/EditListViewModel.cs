namespace Example.FormsApp.Modules.Edit
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Example.FormsApp.Models;
    using Example.FormsApp.Services;

    using Smart.Collections.Generic;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Resolver.Attributes;

    public class EditListViewModel : AppViewModelBase
    {
        [Inject]
        [AllowNull]
        public DataService DataService { get; set; }

        public ObservableCollection<DataEntity> Items { get; } = new();

        public AsyncCommand<DataEntity> SelectCommand { get; }

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
}
