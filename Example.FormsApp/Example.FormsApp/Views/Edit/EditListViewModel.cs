namespace Example.FormsApp.Views.Edit
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class EditListViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> Forward { get; }

        public EditListViewModel()
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }
    }
}
