namespace Smart.Navigation;

public sealed class NavigationParameter : INavigationParameter
{
    private readonly Dictionary<string, object?> values = [];

    public T GetValue<T>(string key)
    {
        return (T)values[key]!;
    }

    public T GetValue<T>()
    {
        return GetValue<T>(typeof(T).Name);
    }

    public T? GetValueOrDefault<T>(string key)
    {
        return values.TryGetValue(key, out var value) ? (T)value! : default;
    }

    public T? GetValueOrDefault<T>()
    {
        return GetValueOrDefault<T>(typeof(T).Name);
    }

    public T GetValueOr<T>(string key, T defaultValue)
    {
        return values.TryGetValue(key, out var value) ? (T)value! : defaultValue!;
    }

    public T GetValueOr<T>(T defaultValue)
    {
        return GetValueOr(typeof(T).Name, defaultValue);
    }

    public bool TryGetValue<T>(string key, out T value)
    {
        var ret = values.TryGetValue(key, out var obj);
        value = ret ? (T)obj! : default!;
        return ret;
    }

    public bool TryGetValue<T>(out T value)
    {
        var ret = values.TryGetValue(typeof(T).Name, out var obj);
        value = ret ? (T)obj! : default!;
        return ret;
    }

    public NavigationParameter SetValue<T>(string key, T value)
    {
        values[key] = value;
        return this;
    }

    public NavigationParameter SetValue<T>(T value)
    {
        return SetValue(typeof(T).Name, value);
    }
}
