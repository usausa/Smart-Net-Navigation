namespace Smart.Navigation.Effects;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

internal abstract class ForwardBackEffect : IWindowsNavigationEffect
{
    private readonly ForwardBackEffectOptions options;

    protected ForwardBackEffect(ForwardBackEffectOptions? options)
    {
        this.options = options ?? new ForwardBackEffectOptions();
    }

    public Task EffectAsync(WindowsNavigationEffectContext context)
    {
        var width = context.Container.ActualWidth;
        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var (from, to) = context.Phase switch
        {
            WindowsNavigationEffectPhase.Open or WindowsNavigationEffectPhase.Activate => (EnterFrom(width), 0d),
            WindowsNavigationEffectPhase.Close or WindowsNavigationEffectPhase.Deactivate => (0d, LeaveTo(width)),
            _ => (0d, 0d)
        };

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (from == to)
        {
            return Task.CompletedTask;
        }

        var tcs = new TaskCompletionSource();
        var anim = new DoubleAnimation(from, to, new Duration(options.Duration))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        anim.Completed += (_, _) =>
        {
            EffectHelper.Restore(context, static view => view.RenderTransform = null);
            tcs.TrySetResult();
        };

        transform.BeginAnimation(TranslateTransform.XProperty, anim);
        return tcs.Task;
    }

    protected abstract double EnterFrom(double width);

    protected abstract double LeaveTo(double width);
}
