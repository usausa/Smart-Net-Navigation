namespace Smart.Navigation;

internal sealed class SlideHorizontalAnimation : IMauiNavigationAnimation
{
    private readonly bool fromRight;

    private readonly uint duration;

    public SlideHorizontalAnimation(bool fromRight, uint durationMilliseconds = 250)
    {
        this.fromRight = fromRight;
        duration = durationMilliseconds;
    }

    public async Task PlayAsync(MauiNavigationAnimationContext context)
    {
        var width = context.Container.Width;
        if (width <= 0)
        {
            return;
        }

        var element = context.View;

        switch (context.Phase)
        {
            case MauiNavigationAnimationPhase.Open:
                element.TranslationX = fromRight ? width : -width;
                await element.TranslateToAsync(0, element.TranslationY, duration, Easing.CubicOut).ConfigureAwait(true);
                element.TranslationX = 0;
                break;

            case MauiNavigationAnimationPhase.Close:
                element.TranslationX = 0;
                await element.TranslateToAsync(fromRight ? -width : width, element.TranslationY, duration, Easing.CubicOut).ConfigureAwait(true);
                element.TranslationX = 0;
                break;

            case MauiNavigationAnimationPhase.Activate:
            case MauiNavigationAnimationPhase.Deactivate:
            default:
                break;
        }
    }
}
