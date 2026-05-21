namespace Smart.Navigation;

public static class MauiNavigationAnimationExtensions
{
    public static NavigationParameter WithForwardAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(MauiAnimationKinds.Forward);

    public static NavigationParameter WithBackAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(MauiAnimationKinds.Back);

    public static NavigationParameter WithPushAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(MauiAnimationKinds.Push);

    public static NavigationParameter WithPopAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(MauiAnimationKinds.Pop);

    public static NavigationParameter WithFadeAnimation(this NavigationParameter parameter)
        => parameter.WithAnimation(MauiAnimationKinds.Fade);
}
