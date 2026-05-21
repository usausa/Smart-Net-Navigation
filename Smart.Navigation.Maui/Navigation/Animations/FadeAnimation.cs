namespace Smart.Navigation;

internal sealed class FadeAnimation : IMauiNavigationAnimation
{
    private readonly uint duration;

    public FadeAnimation(uint durationMilliseconds = 200)
    {
        duration = durationMilliseconds;
    }

    public async Task PlayAsync(MauiNavigationAnimationContext context)
    {
        var element = context.View;

        switch (context.Phase)
        {
            case MauiNavigationAnimationPhase.Open:
            case MauiNavigationAnimationPhase.Activate:
                element.Opacity = 0;
                await element.FadeToAsync(1, duration).ConfigureAwait(true);
                element.Opacity = 1;
                break;

            case MauiNavigationAnimationPhase.Close:
            case MauiNavigationAnimationPhase.Deactivate:
                element.Opacity = 1;
                await element.FadeToAsync(0, duration).ConfigureAwait(true);
                element.Opacity = 1;
                break;

            default:
                break;
        }
    }
}
