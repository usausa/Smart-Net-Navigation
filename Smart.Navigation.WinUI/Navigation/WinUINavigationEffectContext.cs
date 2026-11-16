namespace Smart.Navigation;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

public sealed class WinUINavigationEffectContext
{
    public required Grid Container { get; init; }

    public required FrameworkElement View { get; init; }

    public required WinUINavigationEffectPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}
