namespace Smart.Navigation;

public enum MauiNavigationAnimationPhase
{
    Open,
    Close,
    Activate,
    Deactivate,
}

public sealed class MauiNavigationAnimationContext
{
    public required AbsoluteLayout Container { get; init; }

    public required View View { get; init; }

    public required MauiNavigationAnimationPhase Phase { get; init; }

    public required INavigationParameter Parameter { get; init; }
}

public interface IMauiNavigationAnimation
{
    Task PlayAsync(MauiNavigationAnimationContext context);
}

public static class MauiAnimationKinds
{
    public const string None = "None";
    public const string Forward = "Forward";
    public const string Back = "Back";
    public const string Push = "Push";
    public const string Pop = "Pop";
    public const string Fade = "Fade";
}
