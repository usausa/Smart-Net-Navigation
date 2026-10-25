namespace Smart.Navigation.Effects;

using Smart.Navigation;

internal abstract class ForwardBackEffect : IMauiNavigationEffect
{
    private readonly ForwardBackEffectOptions options;

    protected ForwardBackEffect(ForwardBackEffectOptions? options)
    {
        this.options = options ?? new ForwardBackEffectOptions();
    }

    public async Task EffectAsync(MauiNavigationEffectContext context)
    {
        var width = context.Container.Width;
        if (width <= 0)
        {
            return;
        }

        var (from, to) = context.Phase switch
        {
            MauiNavigationEffectPhase.Open or MauiNavigationEffectPhase.Activate => (EnterFrom(width), 0d),
            MauiNavigationEffectPhase.Close or MauiNavigationEffectPhase.Deactivate => (0d, LeaveTo(width)),
            _ => (0d, 0d)
        };

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (from == to)
        {
            return;
        }

        var element = context.View;
        element.TranslationX = from;
        await element.TranslateToAsync(to, element.TranslationY, (uint)options.Duration.TotalMilliseconds, Easing.CubicOut).ConfigureAwait(true);
        element.TranslationX = 0;
    }

    protected abstract double EnterFrom(double width);

    protected abstract double LeaveTo(double width);
}
