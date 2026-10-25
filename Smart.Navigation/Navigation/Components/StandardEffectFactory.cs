namespace Smart.Navigation.Components;

using System.Diagnostics.CodeAnalysis;

public sealed class StandardEffectFactory : IEffectFactory
{
    public object Create([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type) => EffectFactoryHelper.Create(type, null);
}
