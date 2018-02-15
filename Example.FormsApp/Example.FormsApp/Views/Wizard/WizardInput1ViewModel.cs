namespace Example.FormsApp.Views.Wizard
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class WizardInput1ViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> Forward { get; }

        public WizardInput1ViewModel()
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }
    }
}
