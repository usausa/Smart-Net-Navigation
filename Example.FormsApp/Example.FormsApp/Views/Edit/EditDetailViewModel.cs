namespace Example.FormsApp.Views.Edit
{
    using Example.FormsApp.Services;

    using Smart.Resolver.Attributes;

    public class EditDetailViewModel : AppViewModelBase
    {
        [Inject]
        public DataService DataService { get; set; }
    }
}
