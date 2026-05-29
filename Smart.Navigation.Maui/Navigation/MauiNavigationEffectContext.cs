namespace Smart.Navigation;

public sealed class MauiNavigationEffectContext
{
    public required AbsoluteLayout Container { get; init; }

    public required View View { get; init; }

    public required MauiNavigationEffectPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}
