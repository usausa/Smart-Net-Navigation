namespace Example.WindowsApp.Modules.Wizard;

using Smart.Navigation.Plugins.Scope;

public sealed class WizardContext : IScopeLifecycle, IDisposable
{
    public string Data1 { get; set; } = default!;

    public string Data2 { get; set; } = default!;

    public void OnScopeInitialize()
    {
        System.Diagnostics.Debug.WriteLine("*** WizardContext OnScopeInitialize ***");
    }

    public void OnScopeTerminate()
    {
        System.Diagnostics.Debug.WriteLine("*** WizardContext OnScopeTerminate ***");
    }

    public void Dispose()
    {
        System.Diagnostics.Debug.WriteLine("*** WizardContext Dispose ***");
    }
}
