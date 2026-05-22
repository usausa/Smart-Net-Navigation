namespace Smart.Navigation;

/// <summary>
/// 書き込み可能な NavigationParameter のビュー。
/// 主に IPlugin.OnPrepareParameter から共通パラメータを注入するために使用される。
/// 通常の遷移先 View / ViewModel は INavigationParameter（読み取り専用）を経由してアクセスすること。
/// </summary>
public interface IMutableNavigationParameter : INavigationParameter
{
    /// <summary>キー指定で値を設定する。</summary>
    IMutableNavigationParameter SetValue<T>(string key, T value);

    /// <summary>型名（typeof(T).Name）をキーとして値を設定する。</summary>
    IMutableNavigationParameter SetValue<T>(T value);

    /// <summary>Effect 文字列を設定する。</summary>
    IMutableNavigationParameter WithEffect(string value);
}
