namespace Smart.Navigation.Effects;

using global::Avalonia.Animation;
using global::Avalonia.Animation.Easings;
using global::Avalonia.Media;
using global::Avalonia.Styling;

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

    public async Task PlayAsync(AvaloniaNavigationEffectContext context)
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
            _ => (0d, 0d),
        };

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
                    Setters = { new Setter(TranslateTransform.YProperty, from) },
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters = { new Setter(TranslateTransform.YProperty, to) },
                },
            },
        };

        await animation.RunAsync(transform).ConfigureAwait(true);
        context.View.RenderTransform = null;
    }
}
