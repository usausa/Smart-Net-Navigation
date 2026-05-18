namespace Example.WindowsApp.Animation;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Smart.Navigation;

internal sealed class DropAnimation : IWindowsNavigationAnimation
{
    private readonly TimeSpan duration;

    public DropAnimation(TimeSpan? duration = null)
    {
        this.duration = duration ?? TimeSpan.FromMilliseconds(450);
    }

    public Task PlayAsync(WindowsNavigationAnimationContext context)
    {
        var height = context.Container.ActualHeight;
        if (height <= 0d)
        {
            return Task.CompletedTask;
        }

        var (from, to, easing) = context.Phase switch
        {
            WindowsNavigationAnimationPhase.Open => (-height, 0d, (IEasingFunction)new BounceEase
            {
                Bounces = 2,
                Bounciness = 2,
                EasingMode = EasingMode.EaseOut,
            }),
            WindowsNavigationAnimationPhase.Close => (0d, -height, new CubicEase { EasingMode = EasingMode.EaseIn }),
            _ => (0d, 0d, null!),
        };

        if (from == to)
        {
            return Task.CompletedTask;
        }

        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var tcs = new TaskCompletionSource();
        var anim = new DoubleAnimation(from, to, new Duration(duration))
        {
            EasingFunction = easing,
        };
        anim.Completed += (_, _) =>
        {
            context.View.RenderTransform = null;
            tcs.TrySetResult();
        };

        transform.BeginAnimation(TranslateTransform.YProperty, anim);
        return tcs.Task;
    }
}
