namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal sealed class NoneAvaloniaEffect : IAvaloniaNavigationEffect
{
    public static readonly NoneAvaloniaEffect Instance = new();

    public Task EffectAsync(AvaloniaNavigationEffectContext context) => Task.CompletedTask;
}
