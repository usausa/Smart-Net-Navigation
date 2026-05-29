namespace Smart.Navigation.Effects;

using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Media;
using Avalonia.Styling;

using Smart.Navigation;

internal sealed class SlideVerticalEffect : IAvaloniaNavigationEffect
{
    private readonly bool fromBottom;

    private readonly TimeSpan duration;

    public SlideVerticalEffect(bool fromBottom, TimeSpan? duration = null)
    {
        this.fromBottom = fromBottom;
        this.duration = duration ?? TimeSpan.FromMilliseconds(250);
    }

    public async Task EffectAsync(AvaloniaNavigationEffectContext context)
    {
        var height = context.Container.Bounds.Height;
        if (height <= 0)
        {
            return;
        }

        var (from, to) = context.Phase switch
        {
            AvaloniaNavigationEffectPhase.Open => (fromBottom ? height : -height, 0d),
            AvaloniaNavigationEffectPhase.Close => (0d, fromBottom ? height : -height),
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
                    Setters = { new Setter(TranslateTransform.YProperty, from) }
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters = { new Setter(TranslateTransform.YProperty, to) }
                }
            }
        };

        await animation.RunAsync(transform).ConfigureAwait(true);
        context.View.RenderTransform = null;
    }
}
