namespace Smart.Navigation;

public interface INavigationParameterPrepare : INavigationParameter
{
    void SetValue<T>(string key, T value);

    void SetValue<T>(T value);

    void WithEffect(string value);
}
