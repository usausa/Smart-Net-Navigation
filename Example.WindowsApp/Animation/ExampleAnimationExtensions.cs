namespace Example.WindowsApp.Animation;

using Smart.Navigation;

internal static class ExampleAnimationExtensions
{
    public static NavigationParameter WithDialogAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(ExampleAnimationKinds.Dialog);

    public static NavigationParameter WithZoomAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(ExampleAnimationKinds.Zoom);

    public static NavigationParameter WithDropAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(ExampleAnimationKinds.Drop);

    public static NavigationParameter WithFlipAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(ExampleAnimationKinds.Flip);

    public static NavigationParameter WithRotateAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(ExampleAnimationKinds.Rotate);
}
