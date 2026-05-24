namespace Smart.Navigation;

// 書き込み可能な NavigationParameter のビュー。
// 主に IPlugin.OnPrepareParameter から共通パラメータを注入するために使用される。
// 通常の遷移先 View / ViewModel は INavigationParameter（読み取り専用）を経由してアクセスすること。
public interface IMutableNavigationParameter : INavigationParameter
{
    IMutableNavigationParameter SetValue<T>(string key, T value);

    IMutableNavigationParameter SetValue<T>(T value);

    IMutableNavigationParameter WithEffect(string value);
}
