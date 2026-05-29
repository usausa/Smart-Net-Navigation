namespace Smart.Navigation.Effects;

using System.Windows;
using System.Windows.Media.Animation;

using Smart.Navigation;

internal sealed class FadeEffect : IWindowsNavigationEffect
{
    private readonly TimeSpan duration;

    public FadeEffect(TimeSpan? duration = null)
    {
        this.duration = duration ?? TimeSpan.FromMilliseconds(200);
    }

    public Task EffectAsync(WindowsNavigationEffectContext context)
    {
        var (from, to) = context.Phase switch
        {
            WindowsNavigationEffectPhase.Open or WindowsNavigationEffectPhase.Activate => (0d, 1d),
            WindowsNavigationEffectPhase.Close or WindowsNavigationEffectPhase.Deactivate => (1d, 0d),
            _ => (1d, 1d)
        };

        // ReSharper disable once CompareOfFloatsByEqualityOperator
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
