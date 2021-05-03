namespace Example.FormsApp.Modules
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class MenuViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> ForwardCommand { get; }

        public MenuViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }
    }
}
