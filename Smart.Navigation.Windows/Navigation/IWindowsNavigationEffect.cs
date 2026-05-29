namespace Smart.Navigation;

public interface IWindowsNavigationEffect
{
    Task EffectAsync(WindowsNavigationEffectContext context);
}
