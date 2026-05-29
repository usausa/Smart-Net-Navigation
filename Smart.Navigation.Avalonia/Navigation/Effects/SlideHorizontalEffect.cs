namespace Smart.Navigation.Effects;

using global::Avalonia.Animation;
using global::Avalonia.Animation.Easings;
using global::Avalonia.Media;
using global::Avalonia.Styling;

using Smart.Navigation;

internal sealed class SlideHorizontalEffect : IAvaloniaNavigationEffect
{
    private readonly bool fromRight;

    private readonly TimeSpan duration;

    public SlideHorizontalEffect(bool fromRight, TimeSpan? duration = null)
    {
        this.fromRight = fromRight;
        this.duration = duration ?? TimeSpan.FromMilliseconds(250);
    }

    public async Task EffectAsync(AvaloniaNavigationEffectContext context)
    {
        var width = context.Container.Bounds.Width;
        if (width <= 0)
        {
            return;
        }

        var (from, to) = context.Phase switch
        {
            AvaloniaNavigationEffectPhase.Open => (fromRight ? width : -width, 0d),
            AvaloniaNavigationEffectPhase.Close => (0d, fromRight ? -width : width),
            _ => (0d, 0d)
        };

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (from == to)
        {
            return;
        }

        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var animation = new Animation
        {
            Duration = duration,
            FillMode = FillMode.Forward,
            Easing = new CubicEaseOut(),
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0d),
                    Setters = { new Setter(TranslateTransform.XProperty, from) }
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters = { new Setter(TranslateTransform.XProperty, to) }
                }
            }
        };

        await animation.RunAsync(transform).ConfigureAwait(true);
        context.View.RenderTransform = null;
    }
}
