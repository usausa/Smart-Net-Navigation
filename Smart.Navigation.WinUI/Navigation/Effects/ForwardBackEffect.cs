namespace Smart.Navigation.Effects;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;

internal abstract class ForwardBackEffect : IWinUINavigationEffect
{
    private readonly ForwardBackEffectOptions options;

    protected ForwardBackEffect(ForwardBackEffectOptions? options)
    {
        this.options = options ?? new ForwardBackEffectOptions();
    }

    public Task EffectAsync(WinUINavigationEffectContext context)
    {
        var width = context.Container.ActualWidth;
        if (width <= 0)
        {
            return Task.CompletedTask;
        }

        var (from, to) = context.Phase switch
        {
            WinUINavigationEffectPhase.Open or WinUINavigationEffectPhase.Activate => (EnterFrom(width), 0d),
            _ => (0d, LeaveTo(width))
        };

        var transform = new TranslateTransform();
        context.View.RenderTransform = transform;

        var animation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(options.Duration),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        return EffectHelper.AnimateAsync(context, animation, transform, nameof(TranslateTransform.X), static view => view.RenderTransform = null);
    }

    protected abstract double EnterFrom(double width);

    protected abstract double LeaveTo(double width);
}
