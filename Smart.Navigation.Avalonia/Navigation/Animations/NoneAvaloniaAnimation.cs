namespace Smart.Navigation;

internal sealed class NoneAvaloniaAnimation : IAvaloniaNavigationAnimation
{
    public static readonly NoneAvaloniaAnimation Instance = new();

    public Task PlayAsync(AvaloniaNavigationAnimationContext context) => Task.CompletedTask;
}
