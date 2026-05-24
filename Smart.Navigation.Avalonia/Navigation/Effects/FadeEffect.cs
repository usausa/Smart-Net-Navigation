namespace Smart.Navigation.Effects;

using global::Avalonia;
using global::Avalonia.Animation;
using global::Avalonia.Styling;

using Smart.Navigation;

internal sealed class FadeEffect : IAvaloniaNavigationEffect
{
    private readonly TimeSpan duration;

    public FadeEffect(TimeSpan? duration = null)
    {
        this.duration = duration ?? TimeSpan.FromMilliseconds(200);
    }

    public async Task PlayAsync(AvaloniaNavigationEffectContext context)
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

        var animation = new Animation
        {
            Duration = duration,
            FillMode = FillMode.Forward,
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

        await animation.RunAsync(context.View).ConfigureAwait(true);
        context.View.Opacity = to;
    }
}
