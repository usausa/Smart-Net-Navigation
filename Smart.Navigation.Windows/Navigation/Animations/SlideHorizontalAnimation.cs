namespace Smart.Navigation;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

internal sealed class SlideHorizontalAnimation : IWindowsNavigationAnimation
{
    private readonly bool fromRight;

    private readonly TimeSpan duration;

    public SlideHorizontalAnimation(bool fromRight, TimeSpan? duration = null)
    {
        this.fromRight = fromRight;
        this.duration = duration ?? TimeSpan.FromMilliseconds(250);
    }

    public Task PlayAsync(WindowsNavigationAnimationContext context)
    {
        var width = context.Container.ActualWidth;
        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var (from, to) = context.Phase switch
        {
            WindowsNavigationAnimationPhase.Open => (fromRight ? width : -width, 0d),
            WindowsNavigationAnimationPhase.Close => (0d, fromRight ? -width : width),
            _ => (0d, 0d),
        };

        if (from == to)
        {
            return Task.CompletedTask;
        }

        var tcs = new TaskCompletionSource();
        var anim = new DoubleAnimation(from, to, new Duration(duration))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
        };
        anim.Completed += (_, _) =>
        {
            context.View.RenderTransform = null;
            tcs.TrySetResult();
        };

        transform.BeginAnimation(TranslateTransform.XProperty, anim);
        return tcs.Task;
    }
}
