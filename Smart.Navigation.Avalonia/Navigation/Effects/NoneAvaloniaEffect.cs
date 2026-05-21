namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal sealed class NoneAvaloniaEffect : IAvaloniaNavigationEffect
{
    public static readonly NoneAvaloniaEffect Instance = new();

    public Task PlayAsync(AvaloniaNavigationEffectContext context) => Task.CompletedTask;
}
