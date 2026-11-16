namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Smart.Navigation.Components;

public sealed class WinUINavigationProviderOptions
{
    private readonly Dictionary<string, Func<IEffectFactory, IWinUINavigationEffect>> effectTypes = [with(StringComparer.Ordinal)];

    public bool ClipToBounds { get; set; } = true;

    public bool RestoreFocus { get; set; } = true;

    public IDictionary<string, IWinUINavigationEffect> Effects { get; } = new Dictionary<string, IWinUINavigationEffect>(StringComparer.Ordinal);

    public WinUINavigationProviderOptions RegisterEffect(string key, IWinUINavigationEffect effect)
    {
        effectTypes.Remove(key);
        Effects[key] = effect;
        return this;
    }

    public WinUINavigationProviderOptions RegisterEffect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEffect>(string key)
        where TEffect : IWinUINavigationEffect
    {
        Effects.Remove(key);
        effectTypes[key] = static factory => (IWinUINavigationEffect)factory.Create(typeof(TEffect));
        return this;
    }

    internal Dictionary<string, IWinUINavigationEffect> CreateEffects(IEffectFactory factory)
    {
        var effects = new Dictionary<string, IWinUINavigationEffect>(Effects, StringComparer.Ordinal);
        foreach (var (key, create) in effectTypes)
        {
            if (!Effects.ContainsKey(key))
            {
                effects.Add(key, create(factory));
            }
        }

        return effects;
    }
}
