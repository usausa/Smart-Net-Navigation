namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Data;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class PopEffect : PushPopEffect
{
    public PopEffect(PushPopEffectOptions? options = null)
        : base(options)
    {
    }

    protected override Task EnterAsync(AvaloniaNavigationEffectContext context) => Task.CompletedTask;

    protected override async Task LeaveAsync(AvaloniaNavigationEffectContext context)
    {
        var view = context.View;
        using var front = view.SetValue(Visual.ZIndexProperty, int.MaxValue, BindingPriority.Animation);
        await SlideAsync(view, 0d, context.Container.Bounds.Height, new CubicEaseIn()).ConfigureAwait(true);
    }
}
