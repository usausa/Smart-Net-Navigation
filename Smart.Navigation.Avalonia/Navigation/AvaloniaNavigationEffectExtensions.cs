namespace Smart.Navigation;

public static class AvaloniaNavigationEffectExtensions
{
    public static NavigationParameter WithForwardEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffectKinds.Forward);

    public static NavigationParameter WithBackEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffectKinds.Back);

    public static NavigationParameter WithPushEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffectKinds.Push);

    public static NavigationParameter WithPopEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffectKinds.Pop);

    public static NavigationParameter WithFadeEffect(this NavigationParameter parameter)
        => parameter.WithEffect(AvaloniaEffectKinds.Fade);
}
