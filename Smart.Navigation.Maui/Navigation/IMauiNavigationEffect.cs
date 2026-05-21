namespace Smart.Navigation;

public enum MauiNavigationEffectPhase
{
    Open,
    Close,
    Activate,
    Deactivate,
}

public sealed class MauiNavigationEffectContext
{
    public required AbsoluteLayout Container { get; init; }

    public required View View { get; init; }

    public required MauiNavigationEffectPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}

public interface IMauiNavigationEffect
{
    Task PlayAsync(MauiNavigationEffectContext context);
}

public static class MauiEffectKinds
{
    public const string None = "None";
    public const string Forward = "Forward";
    public const string Back = "Back";
    public const string Push = "Push";
    public const string Pop = "Pop";
    public const string Fade = "Fade";
}
