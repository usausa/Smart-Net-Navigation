namespace Smart.Navigation.Plugins.Scope;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ScopeAttribute : Attribute
{
    public string? Name { get; }

    public Type? RequestType { get; }

    public ScopeAttribute()
    {
    }

    public ScopeAttribute(string name)
    {
        Name = name;
    }

    public ScopeAttribute(Type requestType)
    {
        RequestType = requestType;
    }

    public ScopeAttribute(string name, Type requestType)
    {
        Name = name;
        RequestType = requestType;
    }
}
