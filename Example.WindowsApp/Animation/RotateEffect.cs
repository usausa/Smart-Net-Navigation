namespace Example.WindowsApp.Animation;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Smart.Navigation;

internal sealed class RotateEffect : IWindowsNavigationEffect
{
    private readonly double angle;

    private readonly TimeSpan duration;

    public RotateEffect(double angle = 180d, TimeSpan? duration = null)
    {
        this.angle = angle;
        this.duration = duration ?? TimeSpan.FromMilliseconds(380);
    }

    public Task PlayAsync(WindowsNavigationEffectContext context)
    {
        var (fromAngle, toAngle, fromOpacity, toOpacity) = context.Phase switch
        {
            WindowsNavigationEffectPhase.Open or WindowsNavigationEffectPhase.Activate => (-angle, 0d, 0d, 1d),
            WindowsNavigationEffectPhase.Close or WindowsNavigationEffectPhase.Deactivate => (0d, angle, 1d, 0d),
            _ => (0d, 0d, 1d, 1d),
        };

        if (fromAngle == toAngle && fromOpacity == toOpacity)
        {
            return Task.CompletedTask;
        }

        var rotate = new RotateTransform(fromAngle);
        context.View.RenderTransform = rotate;
        context.View.RenderTransformOrigin = new Point(0.5d, 0.5d);
        context.View.Opacity = fromOpacity;

        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        var rotateAnim = new DoubleAnimation(fromAngle, toAngle, new Duration(duration)) { EasingFunction = easing };
        var opacityAnim = new DoubleAnimation(fromOpacity, toOpacity, new Duration(duration));

        var tcs = new TaskCompletionSource();
        rotateAnim.Completed += (_, _) =>
        {
            context.View.RenderTransform = null;
            context.View.Opacity = 1d;
            tcs.TrySetResult();
        };

        rotate.BeginAnimation(RotateTransform.AngleProperty, rotateAnim);
        context.View.BeginAnimation(UIElement.OpacityProperty, opacityAnim);

        return tcs.Task;
    }
}
