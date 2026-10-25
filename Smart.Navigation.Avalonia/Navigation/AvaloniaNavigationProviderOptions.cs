namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Smart.Navigation.Components;

public sealed class AvaloniaNavigationProviderOptions
{
    private readonly Dictionary<string, Func<IEffectFactory, IAvaloniaNavigationEffect>> effectTypes = [with(StringComparer.Ordinal)];

    public bool RestoreFocus { get; set; } = true;

    public IDictionary<string, IAvaloniaNavigationEffect> Effects { get; } = new Dictionary<string, IAvaloniaNavigationEffect>(StringComparer.Ordinal);

    public AvaloniaNavigationProviderOptions RegisterEffect(string key, IAvaloniaNavigationEffect effect)
    {
        effectTypes.Remove(key);
        Effects[key] = effect;
        return this;
    }

    public AvaloniaNavigationProviderOptions RegisterEffect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEffect>(string key)
        where TEffect : IAvaloniaNavigationEffect
    {
        Effects.Remove(key);
        effectTypes[key] = static factory => (IAvaloniaNavigationEffect)factory.Create(typeof(TEffect));
        return this;
    }

    internal Dictionary<string, IAvaloniaNavigationEffect> CreateEffects(IEffectFactory factory)
    {
        var effects = new Dictionary<string, IAvaloniaNavigationEffect>(Effects, StringComparer.Ordinal);
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
