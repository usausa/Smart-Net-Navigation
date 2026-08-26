namespace Smart.Navigation.Plugins.Scope;

public interface IScopeRequest
{
    string? Name { get; }

    Type? RequestType { get; }
}

[AttributeUsage(AttributeTargets.Property)]
public sealed class ScopeAttribute : Attribute, IScopeRequest
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

[AttributeUsage(AttributeTargets.Property)]
public sealed class ScopeAttribute<TRequest> : Attribute, IScopeRequest
{
    public string? Name { get; }

    public Type RequestType => typeof(TRequest);

    Type IScopeRequest.RequestType => RequestType;

    public ScopeAttribute()
    {
    }

    public ScopeAttribute(string name)
    {
        Name = name;
    }
}
