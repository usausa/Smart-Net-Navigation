namespace Smart.Navigation;

public static class AvaloniaNavigationAnimationExtensions
{
    public static NavigationParameter WithForwardAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(AvaloniaAnimationKinds.Forward);

    public static NavigationParameter WithBackAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(AvaloniaAnimationKinds.Back);

    public static NavigationParameter WithPushAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(AvaloniaAnimationKinds.Push);

    public static NavigationParameter WithPopAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(AvaloniaAnimationKinds.Pop);

    public static NavigationParameter WithFadeAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(AvaloniaAnimationKinds.Fade);
}
