namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

using Smart.Navigation;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class FadeEffect : IMauiNavigationEffect
{
    private readonly FadeEffectOptions options;

    public FadeEffect(FadeEffectOptions? options = null)
    {
        this.options = options ?? new FadeEffectOptions();
    }

    public async Task EffectAsync(MauiNavigationEffectContext context)
    {
        var element = context.View;
        var length = (uint)options.Duration.TotalMilliseconds;

        switch (context.Phase)
        {
            case MauiNavigationEffectPhase.Open:
            case MauiNavigationEffectPhase.Activate:
                element.Opacity = 0;
                await element.FadeToAsync(1, length).ConfigureAwait(true);
                element.Opacity = 1;
                break;
            case MauiNavigationEffectPhase.Close:
            case MauiNavigationEffectPhase.Deactivate:
                element.Opacity = 1;
                await element.FadeToAsync(0, length).ConfigureAwait(true);
                element.Opacity = 1;
                break;
        }
    }
}
