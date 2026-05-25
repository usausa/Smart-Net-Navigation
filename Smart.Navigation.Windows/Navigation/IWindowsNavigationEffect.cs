namespace Smart.Navigation;

using System.Windows;
using System.Windows.Controls;

public enum WindowsNavigationEffectPhase
{
    Open,
    Close,
    Activate,
    Deactivate
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

public static class WindowsEffect
{
    public const string None = nameof(None);
    public const string Forward = nameof(Forward);
    public const string Back = nameof(Back);
    public const string Push = nameof(Push);
    public const string Pop = nameof(Pop);
    public const string Fade = nameof(Fade);
}
