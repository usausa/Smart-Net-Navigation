namespace Example.FormsApp.Modules.Wizard
{
    using System.Threading.Tasks;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Navigation.Plugins.Scope;

    public class WizardResultViewModel : AppViewModelBase, INotifySupportAsync<FunctionKeys>
    {
        [Scope]
        public NotificationValue<WizardContext> Context { get; } = new NotificationValue<WizardContext>();

        public AsyncCommand<ViewId> Forward { get; }

        public WizardResultViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }

        public Task NavigatorNotifyAsync(FunctionKeys parameter)
        {
            switch (parameter)
            {
                case FunctionKeys.Function1:
                    return Navigator.ForwardAsync(ViewId.WizardInput2);
                case FunctionKeys.Function4:
                    return Navigator.ForwardAsync(ViewId.Menu);
                default:
                    return Task.CompletedTask;
            }
        }
    }
}
