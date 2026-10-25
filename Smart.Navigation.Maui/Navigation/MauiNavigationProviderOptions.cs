namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Smart.Navigation.Components;

public sealed class MauiNavigationProviderOptions
{
    private readonly Dictionary<string, Func<IEffectFactory, IMauiNavigationEffect>> effectTypes = [with(StringComparer.Ordinal)];

    public bool RestoreFocus { get; set; } = true;

    public bool DisconnectHandler { get; set; }

    public IDictionary<string, IMauiNavigationEffect> Effects { get; } = new Dictionary<string, IMauiNavigationEffect>(StringComparer.Ordinal);

    public MauiNavigationProviderOptions RegisterEffect(string key, IMauiNavigationEffect effect)
    {
        effectTypes.Remove(key);
        Effects[key] = effect;
        return this;
    }

    public MauiNavigationProviderOptions RegisterEffect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEffect>(string key)
        where TEffect : IMauiNavigationEffect
    {
        Effects.Remove(key);
        effectTypes[key] = static factory => (IMauiNavigationEffect)factory.Create(typeof(TEffect));
        return this;
    }

    internal Dictionary<string, IMauiNavigationEffect> CreateEffects(IEffectFactory factory)
    {
        var effects = new Dictionary<string, IMauiNavigationEffect>(Effects, StringComparer.Ordinal);
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
