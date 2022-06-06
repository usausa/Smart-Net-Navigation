namespace Example.MobileApp.Modules.Wizard;

using Smart;

public sealed class WizardContext : IInitializable, IDisposable
{
    public string Data1 { get; set; } = default!;

    public string Data2 { get; set; } = default!;

    public void Initialize()
    {
        System.Diagnostics.Debug.WriteLine("*** WizardContext Initialize ***");
    }

    public void Dispose()
    {
        System.Diagnostics.Debug.WriteLine("*** WizardContext Dispose ***");
    }
}
