namespace Smart.Navigation.Strategies
{
    public interface INavigationStrategy
    {
        StragtegyResult Initialize(INavigationController controller);

        void UpdateStack(INavigationController controller);

        void PostProcess(INavigationController controller, object previousPage);
    }
}
