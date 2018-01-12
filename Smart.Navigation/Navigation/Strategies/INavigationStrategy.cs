namespace Smart.Navigation.Strategies
{
    public interface INavigationStrategy
    {
        StragtegyResult Initialize(INavigationController controller);

        void Process(INavigationController controller);
    }
}
