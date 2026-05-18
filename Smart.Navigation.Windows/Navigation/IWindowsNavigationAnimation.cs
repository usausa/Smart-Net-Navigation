namespace Smart.Navigation;

using System.Windows;
using System.Windows.Controls;

public enum WindowsNavigationAnimationPhase
{
    Open,
    Close,
    Activate,
    Deactivate,
}

public sealed class WindowsNavigationAnimationContext
{
    public required Canvas Container { get; init; }

    public required FrameworkElement View { get; init; }

    public required WindowsNavigationAnimationPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}

public interface IWindowsNavigationAnimation
{
    Task PlayAsync(WindowsNavigationAnimationContext context);
}

public static class WindowsAnimationKinds
{
    public const string None = "None";
    public const string Forward = "Forward";
    public const string Back = "Back";
    public const string Push = "Push";
    public const string Pop = "Pop";
    public const string Fade = "Fade";
    public const string Dialog = "Dialog";
    public const string Zoom = "Zoom";
    public const string Drop = "Drop";
    public const string Flip = "Flip";
    public const string Rotate = "Rotate";
}
