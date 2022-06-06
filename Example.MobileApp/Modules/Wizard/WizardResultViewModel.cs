namespace Example.MobileApp.Modules.Wizard;

using System.Windows.Input;

using Smart.ComponentModel;
using Smart.Navigation;
using Smart.Navigation.Plugins.Scope;

public class WizardResultViewModel : AppViewModelBase
{
    [Scope]
    public NotificationValue<WizardContext> Context { get; } = new();

    public ICommand ForwardCommand { get; }

    public WizardResultViewModel(ApplicationState applicationState)
        : base(applicationState)
    {
        ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
    }

    protected override Task OnNotifyFunction1Async()
    {
        return Navigator.ForwardAsync(ViewId.WizardInput2);
    }

    protected override Task OnNotifyFunction4Async()
    {
        return Navigator.ForwardAsync(ViewId.Menu);
    }
}
