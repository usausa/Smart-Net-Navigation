namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class PopEffect : PushPopEffect
{
    public PopEffect(PushPopEffectOptions? options = null)
        : base(options)
    {
    }

    protected override Task EnterAsync(MauiNavigationEffectContext context) => Task.CompletedTask;

    protected override async Task LeaveAsync(MauiNavigationEffectContext context)
    {
        var view = context.View;
        var zIndex = view.ZIndex;
        view.ZIndex = int.MaxValue;
        await SlideAsync(view, 0d, context.Container.Height, Easing.CubicIn).ConfigureAwait(true);
        view.ZIndex = zIndex;
    }
}
