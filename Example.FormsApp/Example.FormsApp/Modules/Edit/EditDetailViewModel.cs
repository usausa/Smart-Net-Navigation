namespace Example.FormsApp.Modules.Edit
{
    using System.Threading.Tasks;

    using Example.FormsApp.Models;
    using Example.FormsApp.Services;

    using Smart.ComponentModel;
    using Smart.Navigation;
    using Smart.Resolver.Attributes;

    public class EditDetailViewModel : AppViewModelBase, INavigationEventSupport, INotifySupportAsync<FunctionKeys>
    {
        private DataEntity entity;

        [Inject]
        public DataService DataService { get; set; }

        public NotificationValue<bool> Update { get; } = new NotificationValue<bool>();

        public NotificationValue<string> Name { get; } = new NotificationValue<string>();

        public EditDetailViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }

        public void OnNavigatedFrom(INavigationContext context)
        {
        }

        public void OnNavigatingTo(INavigationContext context)
        {
            Update.Value = Equals(context.ToId, ViewId.EditDetailUpdate);
            if (Update.Value)
            {
                entity = context.Parameter.GetValue<DataEntity>();
                Name.Value = entity.Name;
            }
        }

        public void OnNavigatedTo(INavigationContext context)
        {
        }

        public Task NavigatorNotifyAsync(FunctionKeys parameter)
        {
            switch (parameter)
            {
                case FunctionKeys.Function1:
                    return Navigator.ForwardAsync(ViewId.EditList);
                case FunctionKeys.Function4:
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
                default:
                    return Task.CompletedTask;
            }
        }
    }
}
