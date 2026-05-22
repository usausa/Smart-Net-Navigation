namespace Example.WindowsApp.Animation;

using Smart.Navigation;

internal static class ExampleEffectExtensions
{
    public static NavigationParameter WithDialogOpenEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffect.DialogOpen);

    public static NavigationParameter WithDialogCloseEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffect.DialogClose);

    public static NavigationParameter WithZoomEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffect.Zoom);

    public static NavigationParameter WithDropEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffect.Drop);

    public static NavigationParameter WithFlipEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffect.Flip);

    public static NavigationParameter WithRotateEffect(this NavigationParameter parameter)
        => parameter.WithEffect(ExampleEffect.Rotate);
}
