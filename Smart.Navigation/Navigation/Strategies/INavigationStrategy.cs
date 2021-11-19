namespace Smart.Navigation.Strategies;

public interface INavigationStrategy
{
    StrategyResult? Initialize(INavigationController controller);

    object ResolveToView(INavigationController controller);

    void UpdateStack(INavigationController controller, object toView);
}
