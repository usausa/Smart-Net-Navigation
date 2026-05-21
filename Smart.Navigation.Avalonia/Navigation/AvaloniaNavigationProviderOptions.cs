namespace Smart.Navigation;

public sealed class AvaloniaNavigationProviderOptions
{
    public bool RestoreFocus { get; set; } = true;

    public IDictionary<string, IAvaloniaNavigationAnimation> Animations { get; } =
        new Dictionary<string, IAvaloniaNavigationAnimation>(StringComparer.Ordinal);

    public AvaloniaNavigationProviderOptions RegisterAnimation(string key, IAvaloniaNavigationAnimation animation)
    {
        Animations[key] = animation;
        return this;
    }
}
