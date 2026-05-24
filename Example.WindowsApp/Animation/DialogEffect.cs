namespace Example.WindowsApp.Animation;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Smart.Navigation;

internal sealed class DialogOpenEffect : IWindowsNavigationEffect
{
    private readonly double minScale;
    private readonly TimeSpan duration;

    public DialogOpenEffect(double minScale = 0.7d, TimeSpan? duration = null)
    {
        this.minScale = minScale;
        this.duration = duration ?? TimeSpan.FromMilliseconds(220);
    }

    public Task PlayAsync(WindowsNavigationEffectContext context)
    {
        if (context.Phase is not (WindowsNavigationEffectPhase.Open or WindowsNavigationEffectPhase.Activate))
        {
            return Task.CompletedTask;
        }

        return DialogEffectHelper.PlayScaleFadeAsync(context.View, minScale, 1d, 0d, 1d, duration);
    }
}

internal sealed class DialogCloseEffect : IWindowsNavigationEffect
{
    private readonly double minScale;
    private readonly TimeSpan duration;

    public DialogCloseEffect(double minScale = 0.7d, TimeSpan? duration = null)
    {
        this.minScale = minScale;
        this.duration = duration ?? TimeSpan.FromMilliseconds(220);
    }

    public Task PlayAsync(WindowsNavigationEffectContext context)
    {
        if (context.Phase is not (WindowsNavigationEffectPhase.Close or WindowsNavigationEffectPhase.Deactivate))
        {
            return Task.CompletedTask;
        }

        return DialogEffectHelper.PlayScaleFadeAsync(context.View, 1d, minScale, 1d, 0d, duration);
    }
}

internal static class DialogEffectHelper
{
    internal static Task PlayScaleFadeAsync(
        FrameworkElement view,
        double fromScale,
        double toScale,
        double fromOpacity,
        double toOpacity,
        TimeSpan duration)
    {
        var scale = new ScaleTransform(fromScale, fromScale);
        view.RenderTransform = scale;
        view.RenderTransformOrigin = new Point(0.5d, 0.5d);
        view.Opacity = fromOpacity;

        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        var scaleX = new DoubleAnimation(fromScale, toScale, new Duration(duration)) { EasingFunction = easing };
        var scaleY = new DoubleAnimation(fromScale, toScale, new Duration(duration)) { EasingFunction = easing };
        var opacityAnim = new DoubleAnimation(fromOpacity, toOpacity, new Duration(duration));

        var tcs = new TaskCompletionSource();
        scaleX.Completed += (_, _) =>
        {
            view.RenderTransform = null;
            view.Opacity = 1d;
            tcs.TrySetResult();
        };

        scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleX);
        scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleY);
        view.BeginAnimation(UIElement.OpacityProperty, opacityAnim);

        return tcs.Task;
    }
}
