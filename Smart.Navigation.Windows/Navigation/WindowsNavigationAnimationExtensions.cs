namespace Smart.Navigation;

public static class WindowsNavigationAnimationExtensions
{
    public static NavigationParameter WithForwardAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Forward);

    public static NavigationParameter WithBackAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Back);

    public static NavigationParameter WithPushAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Push);

    public static NavigationParameter WithPopAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Pop);

    public static NavigationParameter WithFadeAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Fade);

    public static NavigationParameter WithDialogAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Dialog);

    public static NavigationParameter WithZoomAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Zoom);

    public static NavigationParameter WithDropAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Drop);

    public static NavigationParameter WithFlipAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Flip);

    public static NavigationParameter WithRotateAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(WindowsAnimationKinds.Rotate);
}
