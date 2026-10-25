namespace Smart.Navigation.Effects;

using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

using Smart.Navigation;

internal abstract class PushPopEffect : IAvaloniaNavigationEffect
{
    protected PushPopEffectOptions Options { get; }

    protected PushPopEffect(PushPopEffectOptions? options)
    {
        Options = options ?? new PushPopEffectOptions();
    }

    public Task EffectAsync(AvaloniaNavigationEffectContext context)
    {
        if (context.Container.Bounds.Height <= 0)
        {
            return Task.CompletedTask;
        }

        return context.Phase switch
        {
            AvaloniaNavigationEffectPhase.Open or AvaloniaNavigationEffectPhase.Activate => EnterAsync(context),
            AvaloniaNavigationEffectPhase.Close or AvaloniaNavigationEffectPhase.Deactivate => LeaveAsync(context),
            _ => Task.CompletedTask
        };
    }

    protected abstract Task EnterAsync(AvaloniaNavigationEffectContext context);

    protected abstract Task LeaveAsync(AvaloniaNavigationEffectContext context);

    protected async Task SlideAsync(Control view, double from, double to, Easing easing)
    {
        view.RenderTransform = new TranslateTransform();

        var animation = new Animation
        {
            Duration = Options.Duration,
            FillMode = FillMode.Forward,
            Easing = easing,
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

        await animation.RunAsync(view).ConfigureAwait(true);
        view.RenderTransform = null;
    }
}
