namespace Smart.Navigation.Components;

using System.Diagnostics.CodeAnalysis;

[RequiresDynamicCode("StandardServiceProvider uses Activator.CreateInstance which is not AOT compatible. Use a DI container instead.")]
[RequiresUnreferencedCode("StandardServiceProvider uses Activator.CreateInstance which may not work with trimming. Use a DI container instead.")]
public sealed class StandardServiceProvider : IServiceProvider
{
    public object? GetService(Type serviceType) => Activator.CreateInstance(serviceType);
}
