namespace Smart.Navigation.Effects;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

internal abstract class PushPopEffect : IWindowsNavigationEffect
{
    protected PushPopEffectOptions Options { get; }

    protected PushPopEffect(PushPopEffectOptions? options)
    {
        Options = options ?? new PushPopEffectOptions();
    }

    public Task EffectAsync(WindowsNavigationEffectContext context)
    {
        return context.Phase switch
        {
            WindowsNavigationEffectPhase.Open or WindowsNavigationEffectPhase.Activate => EnterAsync(context),
            WindowsNavigationEffectPhase.Close or WindowsNavigationEffectPhase.Deactivate => LeaveAsync(context),
            _ => Task.CompletedTask
        };
    }

    protected abstract Task EnterAsync(WindowsNavigationEffectContext context);

    protected abstract Task LeaveAsync(WindowsNavigationEffectContext context);

    protected Task SlideAsync(WindowsNavigationEffectContext context, double from, double to, EasingMode easing, Action<FrameworkElement> restore)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (from == to)
        {
            EffectHelper.Restore(context, restore);
            return Task.CompletedTask;
        }

        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var tcs = new TaskCompletionSource();
        var anim = new DoubleAnimation(from, to, new Duration(Options.Duration))
        {
            EasingFunction = new CubicEase { EasingMode = easing }
        };
        anim.Completed += (_, _) =>
        {
            EffectHelper.Restore(context, restore);
            tcs.TrySetResult();
        };

        transform.BeginAnimation(TranslateTransform.YProperty, anim);
        return tcs.Task;
    }

    protected static void RestoreZIndex(FrameworkElement view, object zIndex)
    {
        if (zIndex == DependencyProperty.UnsetValue)
        {
            view.ClearValue(Panel.ZIndexProperty);
        }
        else
        {
            view.SetValue(Panel.ZIndexProperty, zIndex);
        }
    }
}
