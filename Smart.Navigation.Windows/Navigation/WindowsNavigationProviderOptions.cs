namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Smart.Navigation.Components;

public sealed class WindowsNavigationProviderOptions
{
    private readonly Dictionary<string, Func<IEffectFactory, IWindowsNavigationEffect>> effectTypes = [with(StringComparer.Ordinal)];

    public bool FitToParent { get; set; } = true;

    public bool RestoreFocus { get; set; } = true;

    public IDictionary<string, IWindowsNavigationEffect> Effects { get; } = new Dictionary<string, IWindowsNavigationEffect>(StringComparer.Ordinal);

    public WindowsNavigationProviderOptions RegisterEffect(string key, IWindowsNavigationEffect effect)
    {
        effectTypes.Remove(key);
        Effects[key] = effect;
        return this;
    }

    public WindowsNavigationProviderOptions RegisterEffect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEffect>(string key)
        where TEffect : IWindowsNavigationEffect
    {
        Effects.Remove(key);
        effectTypes[key] = static factory => (IWindowsNavigationEffect)factory.Create(typeof(TEffect));
        return this;
    }

    internal Dictionary<string, IWindowsNavigationEffect> CreateEffects(IEffectFactory factory)
    {
        var effects = new Dictionary<string, IWindowsNavigationEffect>(Effects, StringComparer.Ordinal);
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
