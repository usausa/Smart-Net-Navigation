namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Media.Animation;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class PopEffect : PushPopEffect
{
    public PopEffect(PushPopEffectOptions? options = null)
        : base(options)
    {
    }

    protected override Task EnterAsync(WindowsNavigationEffectContext context) => Task.CompletedTask;

    protected override Task LeaveAsync(WindowsNavigationEffectContext context)
    {
        var zIndex = context.View.ReadLocalValue(Panel.ZIndexProperty);
        Panel.SetZIndex(context.View, int.MaxValue);

        return SlideAsync(context, 0d, context.Container.ActualHeight, EasingMode.EaseIn, view =>
        {
            view.RenderTransform = null;
            RestoreZIndex(view, zIndex);
        });
    }
}
