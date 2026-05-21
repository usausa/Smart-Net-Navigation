namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal sealed class NoneMauiEffect : IMauiNavigationEffect
{
    public static readonly NoneMauiEffect Instance = new();

    public Task PlayAsync(MauiNavigationEffectContext context) => Task.CompletedTask;
}
