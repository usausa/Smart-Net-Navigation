namespace Smart.Navigation
{
    public interface INavigationContext
    {
        object FromPageId { get; }

        object ToPageId { get; }

        bool IsStacked { get; }

        bool IsRestore { get; }

        INavigationParameter Parameter { get; }
    }
}
