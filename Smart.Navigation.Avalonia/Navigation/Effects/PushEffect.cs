namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Data;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class PushEffect : PushPopEffect
{
    public PushEffect(PushPopEffectOptions? options = null)
        : base(options)
    {
    }

    protected override Task EnterAsync(AvaloniaNavigationEffectContext context)
    {
        return SlideAsync(context.View, context.Container.Bounds.Height, 0d, new CubicEaseOut());
    }

    protected override async Task LeaveAsync(AvaloniaNavigationEffectContext context)
    {
        using var behind = context.View.SetValue(Visual.ZIndexProperty, int.MinValue, BindingPriority.Animation);
        await Task.Delay(Options.Duration).ConfigureAwait(true);
    }
}
