namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal sealed class NoneMauiEffect : IMauiNavigationEffect
{
    public static readonly NoneMauiEffect Instance = new();

    public Task EffectAsync(MauiNavigationEffectContext context) => Task.CompletedTask;
}
