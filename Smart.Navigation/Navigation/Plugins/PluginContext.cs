namespace Smart.Navigation.Plugins;

public sealed class PluginContext : IPluginContext
{
    private Dictionary<Type, object?>? store;

    private Dictionary<Type, object?> PreparedStore
    {
        get
        {
            store ??= [];
            return store;
        }
    }

    public void Save<T>(Type type, T value)
    {
        PreparedStore[type] = value;
    }

    public T? Load<T>(Type type)
    {
        if (store is null)
        {
            return default;
        }

        return PreparedStore.TryGetValue(type, out var value) ? (T)value! : default;
    }

    public T LoadOr<T>(Type type, T defaultValue)
    {
        if (store is null)
        {
            return defaultValue;
        }

        return PreparedStore.TryGetValue(type, out var value) ? (T)value! : defaultValue!;
    }

    public T LoadOr<T>(Type type, Func<T> defaultValueFactory)
    {
        if (store is null)
        {
            return defaultValueFactory();
        }

        return PreparedStore.TryGetValue(type, out var value) ? (T)value! : defaultValueFactory();
    }
}
