namespace Example.FormsApp.Modules
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class MenuViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> Forward { get; }

        public MenuViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }
    }
}
