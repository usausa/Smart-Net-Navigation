namespace Example.WindowsApp.Animation;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Smart.Navigation;

internal sealed class FlipEffect : IWindowsNavigationEffect
{
    private readonly TimeSpan duration;

    public FlipEffect(TimeSpan? duration = null)
    {
        this.duration = duration ?? TimeSpan.FromMilliseconds(300);
    }

    public Task PlayAsync(WindowsNavigationEffectContext context)
    {
        var (from, to) = context.Phase switch
        {
            WindowsNavigationEffectPhase.Open or WindowsNavigationEffectPhase.Activate => (0d, 1d),
            WindowsNavigationEffectPhase.Close or WindowsNavigationEffectPhase.Deactivate => (1d, 0d),
            _ => (1d, 1d),
        };

        if (from == to)
        {
            return Task.CompletedTask;
        }

        var scale = new ScaleTransform(from, 1d);
        context.View.RenderTransform = scale;
        context.View.RenderTransformOrigin = new Point(0.5d, 0.5d);

        var tcs = new TaskCompletionSource();
        var anim = new DoubleAnimation(from, to, new Duration(duration))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut },
        };
        anim.Completed += (_, _) =>
        {
            context.View.RenderTransform = null;
            tcs.TrySetResult();
        };

        scale.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
        return tcs.Task;
    }
}
