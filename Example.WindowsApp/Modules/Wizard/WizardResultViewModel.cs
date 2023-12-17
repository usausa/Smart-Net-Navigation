namespace Example.WindowsApp.Modules.Wizard;

using Smart.ComponentModel;
using Smart.Navigation;
using Smart.Navigation.Plugins.Scope;
using Smart.Windows.Input;

public sealed class WizardResultViewModel : AppViewModelBase
{
    [Scope]
    public NotificationValue<WizardContext> Context { get; } = new();

    public AsyncCommand<Type> Forward { get; }

    public WizardResultViewModel(INavigator navigator)
    {
        Forward = MakeAsyncCommand<Type>(navigator.ForwardAsync);
    }
}
