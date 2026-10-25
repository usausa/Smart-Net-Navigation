namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

using Avalonia;
using Avalonia.Animation;
using Avalonia.Data;
using Avalonia.Styling;

using Smart.Navigation;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class FadeEffect : IAvaloniaNavigationEffect
{
    private readonly FadeEffectOptions options;

    public FadeEffect(FadeEffectOptions? options = null)
    {
        this.options = options ?? new FadeEffectOptions();
    }

    public async Task EffectAsync(AvaloniaNavigationEffectContext context)
    {
        var (from, to) = context.Phase switch
        {
            AvaloniaNavigationEffectPhase.Open or AvaloniaNavigationEffectPhase.Activate => (0d, 1d),
            AvaloniaNavigationEffectPhase.Close or AvaloniaNavigationEffectPhase.Deactivate => (1d, 0d),
            _ => (1d, 1d)
        };

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (from == to)
        {
            return;
        }

        var view = context.View;
        using var hold = context.Phase is AvaloniaNavigationEffectPhase.Close or AvaloniaNavigationEffectPhase.Deactivate
            ? view.SetValue(Visual.OpacityProperty, to, BindingPriority.Animation)
            : null;

        var animation = new Animation
        {
            Duration = options.Duration,
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0d),
                    Setters = { new Setter(Visual.OpacityProperty, from) }
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters = { new Setter(Visual.OpacityProperty, to) }
                }
            }
        };

        await animation.RunAsync(view).ConfigureAwait(true);
    }
}
