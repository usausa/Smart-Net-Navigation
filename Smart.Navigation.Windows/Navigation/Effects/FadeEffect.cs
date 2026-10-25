namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media.Animation;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class FadeEffect : IWindowsNavigationEffect
{
    private readonly FadeEffectOptions options;

    public FadeEffect(FadeEffectOptions? options = null)
    {
        this.options = options ?? new FadeEffectOptions();
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
        var anim = new DoubleAnimation(from, to, new Duration(options.Duration));
        anim.Completed += (_, _) =>
        {
            EffectHelper.Restore(context, static view => view.BeginAnimation(UIElement.OpacityProperty, null));
            tcs.TrySetResult();
        };

        context.View.BeginAnimation(UIElement.OpacityProperty, anim);
        return tcs.Task;
    }
}
