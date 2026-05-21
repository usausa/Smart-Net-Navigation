namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal sealed class SlideVerticalEffect : IMauiNavigationEffect
{
    private readonly bool fromBottom;

    private readonly uint duration;

    public SlideVerticalEffect(bool fromBottom, uint durationMilliseconds = 250)
    {
        this.fromBottom = fromBottom;
        duration = durationMilliseconds;
    }

    public async Task PlayAsync(MauiNavigationEffectContext context)
    {
        var height = context.Container.Height;
        if (height <= 0)
        {
            return;
        }

        var element = context.View;

        switch (context.Phase)
        {
            case MauiNavigationEffectPhase.Open:
                element.TranslationY = fromBottom ? height : -height;
                await element.TranslateToAsync(element.TranslationX, 0, duration, Easing.CubicOut).ConfigureAwait(true);
                element.TranslationY = 0;
                break;

            case MauiNavigationEffectPhase.Close:
                element.TranslationY = 0;
                await element.TranslateToAsync(element.TranslationX, fromBottom ? height : -height, duration, Easing.CubicOut).ConfigureAwait(true);
                element.TranslationY = 0;
                break;

            case MauiNavigationEffectPhase.Activate:
            case MauiNavigationEffectPhase.Deactivate:
            default:
                break;
        }
    }
}
