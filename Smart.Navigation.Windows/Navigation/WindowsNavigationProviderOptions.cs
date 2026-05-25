namespace Smart.Navigation;

public sealed class WindowsNavigationProviderOptions
{
    public bool FitToParent { get; set; } = true;

    public bool RestoreFocus { get; set; } = true;

    public IDictionary<string, IWindowsNavigationEffect> Effects { get; } = new Dictionary<string, IWindowsNavigationEffect>(StringComparer.Ordinal);

    public WindowsNavigationProviderOptions RegisterEffect(string key, IWindowsNavigationEffect effect)
    {
        Effects[key] = effect;
        return this;
    }
}
