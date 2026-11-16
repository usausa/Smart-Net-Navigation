namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class PopEffect : PushPopEffect
{
    public PopEffect(PushPopEffectOptions? options = null)
        : base(options)
    {
    }

    protected override Task EnterAsync(WinUINavigationEffectContext context) => Task.CompletedTask;

    protected override Task LeaveAsync(WinUINavigationEffectContext context)
    {
        var zIndex = context.View.ReadLocalValue(Canvas.ZIndexProperty);
        Canvas.SetZIndex(context.View, int.MaxValue);

        return SlideAsync(context, 0d, context.Container.ActualHeight, EasingMode.EaseIn, view =>
        {
            view.RenderTransform = null;
            RestoreZIndex(view, zIndex);
        });
    }
}
