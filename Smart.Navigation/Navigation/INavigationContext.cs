namespace Smart.Navigation;

public interface INavigationContext
{
    object? FromId { get; }

    object ToId { get; }

    NavigationAttributes Attribute { get; }

    INavigationParameter Parameter { get; }
}
