namespace Smart.Navigation.Components;

public sealed class StandardServiceProvider : IServiceProvider
{
    public object? GetService(Type serviceType) => Activator.CreateInstance(serviceType);
}
