namespace Smart.Navigation.Components;

using System.Diagnostics.CodeAnalysis;

[RequiresDynamicCode("StandardActivator uses Activator.CreateInstance which is not AOT compatible. Use a DI container instead.")]
[RequiresUnreferencedCode("StandardActivator uses Activator.CreateInstance which may not work with trimming. Use a DI container instead.")]
public sealed class StandardActivator : IActivator
{
    public object Create(Type type)
    {
        var instance = Activator.CreateInstance(type);
        if (instance is null)
        {
            throw new InvalidOperationException($"Create object failed. type=[{type.FullName}]");
        }

        return instance;
    }
}
