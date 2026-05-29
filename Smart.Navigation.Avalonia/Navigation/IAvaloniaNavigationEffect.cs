namespace Smart.Navigation;

public interface IAvaloniaNavigationEffect
{
    Task EffectAsync(AvaloniaNavigationEffectContext context);
}
