namespace Example.WindowsApp.Animation;

using Smart.Navigation;

internal static class ExampleEffectExtensions
{
    public static NavigationParameter WithDialogEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffectKinds.Dialog);

    public static NavigationParameter WithZoomEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffectKinds.Zoom);

    public static NavigationParameter WithDropEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffectKinds.Drop);

    public static NavigationParameter WithFlipEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffectKinds.Flip);

    public static NavigationParameter WithRotateEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffectKinds.Rotate);
}
