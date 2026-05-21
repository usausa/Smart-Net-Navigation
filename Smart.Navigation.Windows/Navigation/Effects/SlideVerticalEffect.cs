namespace Smart.Navigation.Effects;

using Smart.Navigation;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

internal sealed class SlideVerticalEffect : IWindowsNavigationEffect
{
    private readonly bool fromBottom;

    private readonly TimeSpan duration;

    public SlideVerticalEffect(bool fromBottom, TimeSpan? duration = null)
    {
        this.fromBottom = fromBottom;
        this.duration = duration ?? TimeSpan.FromMilliseconds(250);
    }

    public Task PlayAsync(WindowsNavigationEffectContext context)
    {
        var height = context.Container.ActualHeight;
        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var (from, to) = context.Phase switch
        {
            WindowsNavigationEffectPhase.Open => (fromBottom ? height : -height, 0d),
            WindowsNavigationEffectPhase.Close => (0d, fromBottom ? height : -height),
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

        transform.BeginAnimation(TranslateTransform.YProperty, anim);
        return tcs.Task;
    }
}
