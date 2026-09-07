namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal sealed class SlideHorizontalEffect : IMauiNavigationEffect
{
    private readonly bool fromRight;

    private readonly uint duration;

    public SlideHorizontalEffect(bool fromRight, uint durationMilliseconds = 250)
    {
        this.fromRight = fromRight;
        duration = durationMilliseconds;
    }

    public async Task EffectAsync(MauiNavigationEffectContext context)
    {
        var width = context.Container.Width;
        if (width <= 0)
        {
            return;
        }

        var element = context.View;

        switch (context.Phase)
        {
            case MauiNavigationEffectPhase.Open:
            case MauiNavigationEffectPhase.Activate:
                element.TranslationX = fromRight ? width : -width;
                await element.TranslateToAsync(0, element.TranslationY, duration, Easing.CubicOut).ConfigureAwait(true);
                element.TranslationX = 0;
                break;

            case MauiNavigationEffectPhase.Close:
            case MauiNavigationEffectPhase.Deactivate:
                element.TranslationX = 0;
                await element.TranslateToAsync(fromRight ? -width : width, element.TranslationY, duration, Easing.CubicOut).ConfigureAwait(true);
                element.TranslationX = 0;
                break;
        }
    }
}
