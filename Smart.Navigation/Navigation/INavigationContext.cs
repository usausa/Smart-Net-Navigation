namespace Smart.Navigation
{
    public interface INavigationContext
    {
        object FromId { get; }

        object ToId { get; }

        NavigationAttribute Attribute { get; }

        INavigationParameter Parameter { get; }
    }
}
