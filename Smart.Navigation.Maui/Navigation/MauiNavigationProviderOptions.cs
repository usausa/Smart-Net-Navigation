namespace Smart.Navigation;

public sealed class MauiNavigationProviderOptions
{
    public bool RestoreFocus { get; set; } = true;

    public bool DisconnectHandler { get; set; }

    public IDictionary<string, IMauiNavigationAnimation> Animations { get; } =
        new Dictionary<string, IMauiNavigationAnimation>(StringComparer.Ordinal);

    public MauiNavigationProviderOptions RegisterAnimation(string key, IMauiNavigationAnimation animation)
    {
        Animations[key] = animation;
        return this;
    }
}
