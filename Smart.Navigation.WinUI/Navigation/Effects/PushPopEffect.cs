namespace Smart.Navigation.Effects;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;

internal abstract class PushPopEffect : IWinUINavigationEffect
{
    protected PushPopEffectOptions Options { get; }

    protected PushPopEffect(PushPopEffectOptions? options)
    {
        Options = options ?? new PushPopEffectOptions();
    }

    public Task EffectAsync(WinUINavigationEffectContext context)
    {
        if (context.Container.ActualHeight <= 0)
        {
            return Task.CompletedTask;
        }

        return context.Phase switch
        {
            WinUINavigationEffectPhase.Open or WinUINavigationEffectPhase.Activate => EnterAsync(context),
            WinUINavigationEffectPhase.Close or WinUINavigationEffectPhase.Deactivate => LeaveAsync(context),
            _ => Task.CompletedTask
        };
    }

    protected abstract Task EnterAsync(WinUINavigationEffectContext context);

    protected abstract Task LeaveAsync(WinUINavigationEffectContext context);

    protected Task SlideAsync(WinUINavigationEffectContext context, double from, double to, EasingMode easing, Action<FrameworkElement> restore)
    {
        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var animation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(Options.Duration),
            EasingFunction = new CubicEase { EasingMode = easing }
        };

        return EffectHelper.AnimateAsync(context, animation, transform, nameof(TranslateTransform.Y), restore);
    }

    protected static void RestoreZIndex(FrameworkElement view, object zIndex)
    {
        if (zIndex == DependencyProperty.UnsetValue)
        {
            view.ClearValue(Canvas.ZIndexProperty);
        }
        else
        {
            view.SetValue(Canvas.ZIndexProperty, zIndex);
        }
    }
}
