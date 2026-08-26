namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;

using Smart.Navigation.Components;

public sealed class ActivatorUtilitiesActivator : IActivator
{
    private readonly IServiceProvider serviceProvider;

    public ActivatorUtilitiesActivator(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    [UnconditionalSuppressMessage("Trimming", "IL2067", Justification = "Created types are concrete view or scope object types referenced by the application. AddNavigator is annotated as not trim compatible.")]
    public object Create(Type type) => ActivatorUtilities.CreateInstance(serviceProvider, type);
}
