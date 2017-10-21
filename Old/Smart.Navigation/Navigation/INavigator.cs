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

        // Forward

        void Forward(object id);

        void Forward(object id, INavigationParameter parameter);

        // Push

        void Push(object id);

        void Push(object id, INavigationParameter parameter);

        // Pop

        void Pop();

        void Pop(INavigationParameter parameter);

        void Pop(int level);

        void Pop(int level, INavigationParameter parameter);

        // PopAndForward

        void PopAndForward(object id);

        void PopAndForward(object id, int level);

        void PopAndForward(object id, INavigationParameter parameter);

        void PopAndForward(object id, int level, INavigationParameter parameter);
    }
}
