namespace Smart.Navigation;

public static class MauiNavigationEffectExtensions
{
    public static NavigationParameter WithForwardEffect(this NavigationParameter parameter)
        => parameter.WithEffect(MauiEffect.Forward);

    public static NavigationParameter WithBackEffect(this NavigationParameter parameter)
        => parameter.WithEffect(MauiEffect.Back);

    public static NavigationParameter WithPushEffect(this NavigationParameter parameter)
        => parameter.WithEffect(MauiEffect.Push);

    public static NavigationParameter WithPopEffect(this NavigationParameter parameter)
        => parameter.WithEffect(MauiEffect.Pop);

    public static NavigationParameter WithFadeEffect(this NavigationParameter parameter)
        => parameter.WithEffect(MauiEffect.Fade);
}
