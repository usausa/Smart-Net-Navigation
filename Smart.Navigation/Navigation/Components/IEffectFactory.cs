namespace Smart.Navigation.Components;

using System.Diagnostics.CodeAnalysis;

public interface IEffectFactory
{
    object Create([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type);
}
