namespace Smart.Navigation
{
    using System.Threading.Tasks;

    using Smart.Navigation.Strategies;

    public interface INavigator
    {
        int StackedCount { get; }

        object CurrentViewId { get; }

        object CurrentView { get; }

        object CurrentTarget { get; }

        // Exit

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Ignore")]
        void Exit();

        // Navigate

        bool Navigate(INavigationStrategy strategy, INavigationParameter parameter);

        Task<bool> NavigateAsync(INavigationStrategy strategy, INavigationParameter parameter);
    }
}
