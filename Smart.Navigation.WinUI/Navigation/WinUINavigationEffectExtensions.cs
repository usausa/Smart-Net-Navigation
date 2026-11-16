namespace Smart.Navigation;

public static class WinUINavigationEffectExtensions
{
    public static NavigationParameter WithForwardEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WinUIEffect.Forward);

    public static NavigationParameter WithBackEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WinUIEffect.Back);

    public static NavigationParameter WithPushEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WinUIEffect.Push);

    public static NavigationParameter WithPopEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WinUIEffect.Pop);

    public static NavigationParameter WithFadeEffect(this NavigationParameter parameter)
        => parameter.WithEffect(WinUIEffect.Fade);
}
