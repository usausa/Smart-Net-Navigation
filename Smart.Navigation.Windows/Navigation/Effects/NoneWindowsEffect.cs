namespace Smart.Navigation.Effects;

internal sealed class NoneWindowsEffect : IWindowsNavigationEffect
{
    public static readonly NoneWindowsEffect Instance = new();

    public Task EffectAsync(WindowsNavigationEffectContext context) => Task.CompletedTask;
}
