namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class FadeEffect : IWinUINavigationEffect
{
    private readonly FadeEffectOptions options;

    public FadeEffect(FadeEffectOptions? options = null)
    {
        this.options = options ?? new FadeEffectOptions();
    }

    public Task EffectAsync(WinUINavigationEffectContext context)
    {
        var (from, to) = context.Phase switch
        {
            WinUINavigationEffectPhase.Open or WinUINavigationEffectPhase.Activate => (0d, 1d),
            _ => (1d, 0d)
        };

        var animation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(options.Duration)
        };

        return EffectHelper.AnimateAsync(context, animation, context.View, nameof(UIElement.Opacity), static _ => { });
    }
}
