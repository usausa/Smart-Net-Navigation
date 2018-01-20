namespace Example.WindowsFormsApp.Views.Wizard
{
    using System;

    using Smart;

    public sealed class WizardContext : IInitializable, IDisposable
    {
        public string Data1 { get; set; }

        public string Data2 { get; set; }

        public void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("*** WizardContext Initilize ***");
        }

        public void Dispose()
        {
            System.Diagnostics.Debug.WriteLine("*** WizardContext Dispose ***");
        }
    }
}
