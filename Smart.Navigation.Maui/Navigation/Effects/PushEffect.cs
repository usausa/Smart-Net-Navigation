namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class PushEffect : PushPopEffect
{
    public PushEffect(PushPopEffectOptions? options = null)
        : base(options)
    {
    }

    protected override Task EnterAsync(MauiNavigationEffectContext context)
    {
        return SlideAsync(context.View, context.Container.Height, 0d, Easing.CubicOut);
    }

    protected override async Task LeaveAsync(MauiNavigationEffectContext context)
    {
        var view = context.View;
        var zIndex = view.ZIndex;
        view.ZIndex = int.MinValue;
        await Task.Delay(Options.Duration).ConfigureAwait(true);
        view.ZIndex = zIndex;
    }
}
