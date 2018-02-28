namespace Example.FormsApp.Modules.Edit
{
    using Example.FormsApp.Services;

    using Smart.Resolver.Attributes;

    public class EditDetailViewModel : AppViewModelBase
    {
        [Inject]
        public DataService DataService { get; set; }

        public EditDetailViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
        }
    }
}
