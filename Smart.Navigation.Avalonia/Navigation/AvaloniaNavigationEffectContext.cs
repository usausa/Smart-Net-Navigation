namespace Smart.Navigation;

using Avalonia.Controls;

public sealed class AvaloniaNavigationEffectContext
{
    public required Grid Container { get; init; }

    public required Control View { get; init; }

    public required AvaloniaNavigationEffectPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}
