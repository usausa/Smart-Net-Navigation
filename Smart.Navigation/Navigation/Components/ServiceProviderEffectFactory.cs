namespace Smart.Navigation.Components;

using System.Diagnostics.CodeAnalysis;

public sealed class ServiceProviderEffectFactory : IEffectFactory
{
    private readonly IServiceProvider serviceProvider;

    public ServiceProviderEffectFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public object Create([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type) => EffectFactoryHelper.Create(type, serviceProvider.GetService);
}
