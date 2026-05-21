namespace Smart.Navigation;

internal sealed class SlideVerticalAnimation : IMauiNavigationAnimation
{
    private readonly bool fromBottom;

    private readonly uint duration;

    public SlideVerticalAnimation(bool fromBottom, uint durationMilliseconds = 250)
    {
        this.fromBottom = fromBottom;
        duration = durationMilliseconds;
    }

    public async Task PlayAsync(MauiNavigationAnimationContext context)
    {
        var height = context.Container.Height;
        if (height <= 0)
        {
            return;
        }

        var element = context.View;

        switch (context.Phase)
        {
            case MauiNavigationAnimationPhase.Open:
                element.TranslationY = fromBottom ? height : -height;
                await element.TranslateToAsync(element.TranslationX, 0, duration, Easing.CubicOut).ConfigureAwait(true);
                element.TranslationY = 0;
                break;

            case MauiNavigationAnimationPhase.Close:
                element.TranslationY = 0;
                await element.TranslateToAsync(element.TranslationX, fromBottom ? height : -height, duration, Easing.CubicOut).ConfigureAwait(true);
                element.TranslationY = 0;
                break;

            case MauiNavigationAnimationPhase.Activate:
            case MauiNavigationAnimationPhase.Deactivate:
            default:
                break;
        }
    }
}
