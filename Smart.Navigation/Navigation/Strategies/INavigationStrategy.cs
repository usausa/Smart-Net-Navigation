namespace Smart.Navigation.Strategies
{
    public interface INavigationStrategy
    {
        // TODO WithController
        object ToId { get; }

        // TODO WithController
        NavigationAttribute Attribute { get; }

        void Process(INavigationController controller);
    }
}
