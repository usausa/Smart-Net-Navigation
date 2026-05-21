namespace Smart.Navigation;

using System.Windows;
using System.Windows.Controls;

public enum WindowsNavigationEffectPhase
{
    Open,
    Close,
    Activate,
    Deactivate,
}

public sealed class WindowsNavigationEffectContext
{
    public required Canvas Container { get; init; }

    public required FrameworkElement View { get; init; }

    public required WindowsNavigationEffectPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}

public interface IWindowsNavigationEffect
{
    Task PlayAsync(WindowsNavigationEffectContext context);
}

public static class WindowsEffectKinds
{
    public const string None = "None";
    public const string Forward = "Forward";
    public const string Back = "Back";
    public const string Push = "Push";
    public const string Pop = "Pop";
    public const string Fade = "Fade";
}
