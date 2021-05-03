namespace Example.FormsApp.Modules.Wizard
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Smart;

    public sealed class WizardContext : IInitializable, IDisposable
    {
        [AllowNull]
        public string Data1 { get; set; }

        [AllowNull]
        public string Data2 { get; set; }

        public void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("*** WizardContext Initialize ***");
        }

        public void Dispose()
        {
            System.Diagnostics.Debug.WriteLine("*** WizardContext Dispose ***");
        }
    }
}
