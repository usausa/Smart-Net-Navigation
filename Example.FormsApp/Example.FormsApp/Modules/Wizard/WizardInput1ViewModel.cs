namespace Example.FormsApp.Modules.Wizard
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardInput1ViewModel : AppViewModelBase
    {
        public static WizardInput1ViewModel DesignInstance => null; // For design

        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new NotificationValue<WizardContext>();

        public AsyncCommand<ViewId> ForwardCommand { get; }

        public WizardInput1ViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }

        protected override Task OnNotifyFunction1Async()
        {
            return Navigator.ForwardAsync(ViewId.Menu);
        }

        protected override Task OnNotifyFunction4Async()
        {
            return Navigator.ForwardAsync(ViewId.WizardInput2);
        }
    }
}
