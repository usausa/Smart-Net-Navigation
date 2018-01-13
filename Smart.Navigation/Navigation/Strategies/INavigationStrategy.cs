namespace Smart.Navigation.Strategies
{
    public interface INavigationStrategy
    {
        StragtegyResult Initialize(INavigationController controller);

        object ResolveToPage(INavigationController controller);

        void UpdateStack(INavigationController controller, object toPage);
    }
}
