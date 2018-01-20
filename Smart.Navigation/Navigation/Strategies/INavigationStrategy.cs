namespace Smart.Navigation.Strategies
{
    public interface INavigationStrategy
    {
        StragtegyResult Initialize(INavigationController controller);

        object ResolveToView(INavigationController controller);

        void UpdateStack(INavigationController controller, object toView);
    }
}
