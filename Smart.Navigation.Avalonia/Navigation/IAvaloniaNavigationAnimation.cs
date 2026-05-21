namespace Smart.Navigation;

using global::Avalonia.Controls;

public enum AvaloniaNavigationAnimationPhase
{
    Open,
    Close,
    Activate,
    Deactivate,
}

public sealed class AvaloniaNavigationAnimationContext
{
    public required Canvas Container { get; init; }

    public required Control View { get; init; }

    public required AvaloniaNavigationAnimationPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}

public interface IAvaloniaNavigationAnimation
{
    Task PlayAsync(AvaloniaNavigationAnimationContext context);
}

public static class AvaloniaAnimationKinds
{
    public const string None = "None";
    public const string Forward = "Forward";
    public const string Back = "Back";
    public const string Push = "Push";
    public const string Pop = "Pop";
    public const string Fade = "Fade";
}
