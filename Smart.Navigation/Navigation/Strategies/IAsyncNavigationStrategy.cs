namespace Smart.Navigation.Strategies;

public interface IAsyncNavigationStrategy : INavigationStrategy
{
    Task UpdateStackAsync(IAsyncNavigationController controller, object toView, INavigationParameter parameter);
}
