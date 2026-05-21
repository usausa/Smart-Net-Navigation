namespace Smart.Navigation;

using global::Avalonia;
using global::Avalonia.Animation;
using global::Avalonia.Styling;

internal sealed class FadeAnimation : IAvaloniaNavigationAnimation
{
    private readonly TimeSpan duration;

    public FadeAnimation(TimeSpan? duration = null)
    {
        this.duration = duration ?? TimeSpan.FromMilliseconds(200);
    }

    public async Task PlayAsync(AvaloniaNavigationAnimationContext context)
    {
        var (from, to) = context.Phase switch
        {
            AvaloniaNavigationAnimationPhase.Open or AvaloniaNavigationAnimationPhase.Activate => (0d, 1d),
            AvaloniaNavigationAnimationPhase.Close or AvaloniaNavigationAnimationPhase.Deactivate => (1d, 0d),
            _ => (1d, 1d),
        };

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
                    Setters = { new Setter(Visual.OpacityProperty, from) },
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters = { new Setter(Visual.OpacityProperty, to) },
                },
            },
        };

        await animation.RunAsync(context.View).ConfigureAwait(true);
        context.View.Opacity = to;
    }
}
