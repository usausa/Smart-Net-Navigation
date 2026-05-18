namespace Smart.Navigation;

public sealed class WindowsNavigationProviderOptions
{
    public bool FitToParent { get; set; } = true;

    public bool RestoreFocus { get; set; } = true;

    public IDictionary<string, IWindowsNavigationAnimation> Animations { get; } =
        new Dictionary<string, IWindowsNavigationAnimation>(StringComparer.Ordinal);

    public WindowsNavigationProviderOptions RegisterAnimation(string key, IWindowsNavigationAnimation animation)
    {
        Animations[key] = animation;
        return this;
    }
}
