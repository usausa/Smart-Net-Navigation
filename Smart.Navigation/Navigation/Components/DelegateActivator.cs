namespace Smart.Navigation.Components;

public sealed class DelegateActivator : IActivator
{
    private readonly Func<Type, object?> callback;

    public DelegateActivator(Func<Type, object?> callback)
    {
        this.callback = callback;
    }

    public object Create(Type type)
    {
        var instance = callback(type);
        if (instance is null)
        {
            throw new InvalidOperationException($"Create object failed. type=[{type.FullName}]");
        }

        return instance;
    }
}
