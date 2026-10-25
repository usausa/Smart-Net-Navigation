namespace Smart.Navigation.Effects;

using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Media;
using Avalonia.Styling;

using Smart.Navigation;

internal abstract class ForwardBackEffect : IAvaloniaNavigationEffect
{
    private readonly ForwardBackEffectOptions options;

    protected ForwardBackEffect(ForwardBackEffectOptions? options)
    {
        this.options = options ?? new ForwardBackEffectOptions();
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
            AvaloniaNavigationEffectPhase.Open or AvaloniaNavigationEffectPhase.Activate => (EnterFrom(width), 0d),
            AvaloniaNavigationEffectPhase.Close or AvaloniaNavigationEffectPhase.Deactivate => (0d, LeaveTo(width)),
            _ => (0d, 0d)
        };

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (from == to)
        {
            return;
        }

        var view = context.View;
        view.RenderTransform = new TranslateTransform();

        var animation = new Animation
        {
            Duration = options.Duration,
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

        await animation.RunAsync(view).ConfigureAwait(true);
        view.RenderTransform = null;
    }

    protected abstract double EnterFrom(double width);

    protected abstract double LeaveTo(double width);
}
