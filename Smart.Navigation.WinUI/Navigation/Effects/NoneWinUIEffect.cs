namespace Smart.Navigation.Effects;

internal sealed class NoneWinUIEffect : IWinUINavigationEffect
{
    public static readonly NoneWinUIEffect Instance = new();

    public Task EffectAsync(WinUINavigationEffectContext context) => Task.CompletedTask;
}
