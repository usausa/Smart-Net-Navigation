namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal abstract class PushPopEffect : IMauiNavigationEffect
{
    protected PushPopEffectOptions Options { get; }

    protected PushPopEffect(PushPopEffectOptions? options)
    {
        Options = options ?? new PushPopEffectOptions();
    }

    public Task EffectAsync(MauiNavigationEffectContext context)
    {
        if (context.Container.Height <= 0)
        {
            return Task.CompletedTask;
        }

        return context.Phase switch
        {
            MauiNavigationEffectPhase.Open or MauiNavigationEffectPhase.Activate => EnterAsync(context),
            MauiNavigationEffectPhase.Close or MauiNavigationEffectPhase.Deactivate => LeaveAsync(context),
            _ => Task.CompletedTask
        };
    }

    protected abstract Task EnterAsync(MauiNavigationEffectContext context);

    protected abstract Task LeaveAsync(MauiNavigationEffectContext context);

    protected async Task SlideAsync(View view, double from, double to, Easing easing)
    {
        view.TranslationY = from;
        await view.TranslateToAsync(view.TranslationX, to, (uint)Options.Duration.TotalMilliseconds, easing).ConfigureAwait(true);
        view.TranslationY = 0;
    }
}
