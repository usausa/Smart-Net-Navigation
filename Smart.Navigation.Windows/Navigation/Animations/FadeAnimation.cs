namespace Smart.Navigation;

using System.Windows;
using System.Windows.Media.Animation;

internal sealed class FadeAnimation : IWindowsNavigationAnimation
{
    private readonly TimeSpan duration;

    public FadeAnimation(TimeSpan? duration = null)
    {
        this.duration = duration ?? TimeSpan.FromMilliseconds(200);
    }

    public Task PlayAsync(WindowsNavigationAnimationContext context)
    {
        var (from, to) = context.Phase switch
        {
            WindowsNavigationAnimationPhase.Open or WindowsNavigationAnimationPhase.Activate => (0d, 1d),
            WindowsNavigationAnimationPhase.Close or WindowsNavigationAnimationPhase.Deactivate => (1d, 0d),
            _ => (1d, 1d),
        };

        if (from == to)
        {
            return Task.CompletedTask;
        }

        var tcs = new TaskCompletionSource();
        var anim = new DoubleAnimation(from, to, new Duration(duration));
        anim.Completed += (_, _) => tcs.TrySetResult();

        context.View.BeginAnimation(UIElement.OpacityProperty, anim);
        return tcs.Task;
    }
}
