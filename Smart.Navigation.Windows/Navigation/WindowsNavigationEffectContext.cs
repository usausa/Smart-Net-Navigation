namespace Smart.Navigation;

using System.Windows;
using System.Windows.Controls;

public sealed class WindowsNavigationEffectContext
{
    public required Canvas Container { get; init; }

    public required FrameworkElement View { get; init; }

    public required WindowsNavigationEffectPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}
