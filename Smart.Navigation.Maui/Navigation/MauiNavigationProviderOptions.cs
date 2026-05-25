namespace Smart.Navigation;

public sealed class MauiNavigationProviderOptions
{
    public bool RestoreFocus { get; set; } = true;

    public bool DisconnectHandler { get; set; }

    public IDictionary<string, IMauiNavigationEffect> Effects { get; } = new Dictionary<string, IMauiNavigationEffect>(StringComparer.Ordinal);

    public MauiNavigationProviderOptions RegisterEffect(string key, IMauiNavigationEffect effect)
    {
        Effects[key] = effect;
        return this;
    }
}
