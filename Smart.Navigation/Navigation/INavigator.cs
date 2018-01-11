namespace Smart.Navigation
{
    using Smart.Navigation.Strategies;

    public interface INavigator
    {
        int StackedCount { get; }

        object CurrentPageId { get; }

        object CurrentPageDomain { get; }

        object CurrentPage { get; }

        object CurrentTarget { get; }

        // Exit

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Ignore")]
        void Exit();

        // Navigate

        void Navigate(INavigationStrategy strategy, INavigationParameter parameter);
    }
}
