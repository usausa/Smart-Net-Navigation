namespace Smart.Navigation.Components;

public sealed class ServiceProviderActivator : IActivator
{
    private readonly IServiceProvider serviceProvider;

    public ServiceProviderActivator(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public object Create(Type type)
    {
        var instance = serviceProvider.GetService(type);
        if (instance is null)
        {
            throw new InvalidOperationException($"Create object failed. type=[{type.FullName}]");
        }

        return instance;
    }
}
