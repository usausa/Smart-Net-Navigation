namespace Smart.Navigation;

public sealed class NavigationContext : INavigationContext
{
    public object? FromId { get; }

    public object ToId { get; }

    public NavigationAttributes Attribute { get; }

    public INavigationParameter Parameter { get; }

    public NavigationContext(object? fromId, object toId, NavigationAttributes attribute, INavigationParameter parameter)
    {
        FromId = fromId;
        ToId = toId;
        Attribute = attribute;
        Parameter = parameter;
    }
}
