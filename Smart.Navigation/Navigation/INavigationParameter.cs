namespace Smart.Navigation;

public interface INavigationParameter
{
    T GetValue<T>(string key);

    T GetValue<T>();

    T? GetValueOrDefault<T>(string key);

    T? GetValueOrDefault<T>();

    T GetValueOr<T>(string key, T defaultValue);

    T GetValueOr<T>(T defaultValue);

    bool TryGetValue<T>(string key, out T value);

    bool TryGetValue<T>(out T value);
}
