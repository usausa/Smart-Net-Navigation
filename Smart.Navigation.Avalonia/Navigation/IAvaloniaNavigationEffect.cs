namespace Smart.Navigation;

using global::Avalonia.Controls;

public enum AvaloniaNavigationEffectPhase
{
    Open,
    Close,
    Activate,
    Deactivate,
}

public sealed class AvaloniaNavigationEffectContext
{
    public required Canvas Container { get; init; }

    public required Control View { get; init; }

    public required AvaloniaNavigationEffectPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}

public interface IAvaloniaNavigationEffect
{
    Task PlayAsync(AvaloniaNavigationEffectContext context);
}

public static class AvaloniaEffect
{
    public const string None = "None";
    public const string Forward = "Forward";
    public const string Back = "Back";
    public const string Push = "Push";
    public const string Pop = "Pop";
    public const string Fade = "Fade";
}
