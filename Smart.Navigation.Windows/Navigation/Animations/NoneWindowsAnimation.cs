namespace Smart.Navigation;

internal sealed class NoneWindowsAnimation : IWindowsNavigationAnimation
{
    public static readonly NoneWindowsAnimation Instance = new();

    public Task PlayAsync(WindowsNavigationAnimationContext context) => Task.CompletedTask;
}
