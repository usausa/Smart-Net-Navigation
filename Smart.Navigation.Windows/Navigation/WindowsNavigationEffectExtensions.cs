namespace Smart.Navigation;

public static class WindowsNavigationEffectExtensions
{
    public static NavigationParameter WithForwardEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WindowsEffect.Forward);

    public static NavigationParameter WithBackEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WindowsEffect.Back);

    public static NavigationParameter WithPushEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WindowsEffect.Push);

    public static NavigationParameter WithPopEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WindowsEffect.Pop);

    public static NavigationParameter WithFadeEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WindowsEffect.Fade);
}
