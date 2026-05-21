namespace Smart.Navigation;

internal sealed class NoneMauiAnimation : IMauiNavigationAnimation
{
    public static readonly NoneMauiAnimation Instance = new();

    public Task PlayAsync(MauiNavigationAnimationContext context) => Task.CompletedTask;
}
