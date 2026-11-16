namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class PushEffect : PushPopEffect
{
    public PushEffect(PushPopEffectOptions? options = null)
        : base(options)
    {
    }

    protected override Task EnterAsync(WinUINavigationEffectContext context)
    {
        return SlideAsync(context, context.Container.ActualHeight, 0d, EasingMode.EaseOut, static view => view.RenderTransform = null);
    }

    protected override async Task LeaveAsync(WinUINavigationEffectContext context)
    {
        var zIndex = context.View.ReadLocalValue(Canvas.ZIndexProperty);
        Canvas.SetZIndex(context.View, int.MinValue);

        await Task.Delay(Options.Duration).ConfigureAwait(true);

        EffectHelper.Restore(context, view => RestoreZIndex(view, zIndex));
    }
}
