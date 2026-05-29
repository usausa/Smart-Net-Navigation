namespace Smart.Navigation;

public interface IMauiNavigationEffect
{
    Task EffectAsync(MauiNavigationEffectContext context);
}
