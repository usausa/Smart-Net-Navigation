namespace Example.FormsApp.Modules.Debug
{
    using Smart.Navigation.Attributes;

    [View(ViewId.DebugLeak)]
    public partial class DebugLeakView
    {
        public DebugLeakView()
        {
            InitializeComponent();
        }

        ~DebugLeakView()
        {
            System.Diagnostics.Debug.WriteLine("~DebugLeakView()");
        }
    }
}