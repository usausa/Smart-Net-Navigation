namespace Example.WindowsApp.Animation;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Smart.Navigation;

internal sealed class ZoomAnimation : IWindowsNavigationAnimation
{
    private readonly double overshoot;

    private readonly TimeSpan duration;

    public ZoomAnimation(double overshoot = 1.3d, TimeSpan? duration = null)
    {
        this.overshoot = overshoot;
        this.duration = duration ?? TimeSpan.FromMilliseconds(240);
    }

    public Task PlayAsync(WindowsNavigationAnimationContext context)
    {
        var (fromScale, toScale, fromOpacity, toOpacity) = context.Phase switch
        {
            WindowsNavigationAnimationPhase.Open or WindowsNavigationAnimationPhase.Activate => (overshoot, 1d, 0d, 1d),
            WindowsNavigationAnimationPhase.Close or WindowsNavigationAnimationPhase.Deactivate => (1d, overshoot, 1d, 0d),
            _ => (1d, 1d, 1d, 1d),
        };

        if (fromScale == toScale && fromOpacity == toOpacity)
        {
            return Task.CompletedTask;
        }

        var scale = new ScaleTransform(fromScale, fromScale);
        context.View.RenderTransform = scale;
        context.View.RenderTransformOrigin = new Point(0.5d, 0.5d);
        context.View.Opacity = fromOpacity;

        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        var scaleX = new DoubleAnimation(fromScale, toScale, new Duration(duration)) { EasingFunction = easing };
        var scaleY = new DoubleAnimation(fromScale, toScale, new Duration(duration)) { EasingFunction = easing };
        var opacityAnim = new DoubleAnimation(fromOpacity, toOpacity, new Duration(duration));

        var tcs = new TaskCompletionSource();
        scaleX.Completed += (_, _) =>
        {
            context.View.RenderTransform = null;
            context.View.Opacity = 1d;
            tcs.TrySetResult();
        };

        scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleX);
        scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleY);
        context.View.BeginAnimation(UIElement.OpacityProperty, opacityAnim);

        return tcs.Task;
    }
}
