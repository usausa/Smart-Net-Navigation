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
}
