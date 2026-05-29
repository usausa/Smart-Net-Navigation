namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal sealed class FadeEffect : IMauiNavigationEffect
{
    private readonly uint duration;

    public FadeEffect(uint durationMilliseconds = 200)
    {
        duration = durationMilliseconds;
    }

    public async Task EffectAsync(MauiNavigationEffectContext context)
    {
        var element = context.View;

        switch (context.Phase)
        {
            case MauiNavigationEffectPhase.Open:
            case MauiNavigationEffectPhase.Activate:
                element.Opacity = 0;
                await element.FadeToAsync(1, duration).ConfigureAwait(true);
                element.Opacity = 1;
                break;
            case MauiNavigationEffectPhase.Close:
            case MauiNavigationEffectPhase.Deactivate:
                element.Opacity = 1;
                await element.FadeToAsync(0, duration).ConfigureAwait(true);
                element.Opacity = 1;
                break;
        }
    }
}
