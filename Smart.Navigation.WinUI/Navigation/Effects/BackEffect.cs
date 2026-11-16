namespace Smart.Navigation.Effects;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created by RegisterEffect<TEffect>().")]
internal sealed class BackEffect : ForwardBackEffect
{
    public BackEffect(ForwardBackEffectOptions? options = null)
        : base(options)
    {
    }

    protected override double EnterFrom(double width) => -width;

    protected override double LeaveTo(double width) => width;
}
