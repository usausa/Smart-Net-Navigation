namespace Smart.Navigation.Plugins.Scope;

public sealed class ScopeProperty
{
    public string Name { get; }

    public Type RequestType { get; }

    public Action<object?, object?> Setter { get; }

    public ScopeProperty(
        string name,
        Type requestType,
        Action<object?, object?> setter)
    {
        Name = name;
        RequestType = requestType;
        Setter = setter;
    }
}
