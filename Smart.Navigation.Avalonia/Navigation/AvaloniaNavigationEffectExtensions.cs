namespace Smart.Navigation;

public static class AvaloniaNavigationEffectExtensions
{
    public static NavigationParameter WithForwardEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffect.Forward);

    public static NavigationParameter WithBackEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffect.Back);

    public static NavigationParameter WithPushEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffect.Push);

    public static NavigationParameter WithPopEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffect.Pop);

    public static NavigationParameter WithFadeEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffect.Fade);
}
