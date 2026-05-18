namespace Example.WindowsApp.Animation;

// Application-side animation kinds. The Smart.Navigation.Windows package ships with
// the basic kinds (Forward / Back / Push / Pop / Fade / None) only; this class shows
// how an application can add its own kinds and register the corresponding handlers
// at provider configuration time.
internal static class ExampleAnimationKinds
{
    public const string Dialog = "Dialog";
    public const string Zoom = "Zoom";
    public const string Drop = "Drop";
    public const string Flip = "Flip";
    public const string Rotate = "Rotate";
}
