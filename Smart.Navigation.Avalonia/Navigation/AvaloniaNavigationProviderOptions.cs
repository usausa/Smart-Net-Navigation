namespace Smart.Navigation;

public sealed class AvaloniaNavigationProviderOptions
{
    public bool RestoreFocus { get; set; } = true;

    public IDictionary<string, IAvaloniaNavigationEffect> Effects { get; } = new Dictionary<string, IAvaloniaNavigationEffect>(StringComparer.Ordinal);

    public AvaloniaNavigationProviderOptions RegisterEffect(string key, IAvaloniaNavigationEffect effect)
    {
        Effects[key] = effect;
        return this;
    }
}
