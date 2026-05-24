namespace Example.WindowsApp.Animation;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Smart.Navigation;

internal sealed class DropEffect : IWindowsNavigationEffect
{
    private readonly TimeSpan duration;

    public DropEffect(TimeSpan? duration = null)
    {
        this.duration = duration ?? TimeSpan.FromMilliseconds(450);
    }

    public Task PlayAsync(WindowsNavigationEffectContext context)
    {
        var height = context.Container.ActualHeight;
        if (height <= 0d)
        {
            return Task.CompletedTask;
        }

        var (from, to, easing) = context.Phase switch
        {
            WindowsNavigationEffectPhase.Open => (-height, 0d, (IEasingFunction)new BounceEase
            {
                Bounces = 2,
                Bounciness = 2,
                EasingMode = EasingMode.EaseOut
            }),
            WindowsNavigationEffectPhase.Close => (0d, -height, new CubicEase { EasingMode = EasingMode.EaseIn }),
            _ => (0d, 0d, null!)
        };

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (from == to)
        {
            return Task.CompletedTask;
        }

        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var tcs = new TaskCompletionSource();
        var anim = new DoubleAnimation(from, to, new Duration(duration))
        {
            EasingFunction = easing
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
