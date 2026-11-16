namespace Smart.Navigation;

public interface IWinUINavigationEffect
{
    Task EffectAsync(WinUINavigationEffectContext context);
}
