namespace Smart.Navigation
{
    public interface INavigator
    {
        // Exit

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Ignore")]
        void Exit();

        // Forward

        void Forward(object id);

        void Forward(object id, INavigationParameter parameters);

        // Push

        void Push(object id);

        void Push(object id, INavigationParameter parameters);

        // Pop

        void Pop();

        void Pop(INavigationParameter parameters);

        void Pop(int level);

        void Pop(int level, INavigationParameter parameters);

        // PopAndForward

        void PopAndForward(object id);

        void PopAndForward(object id, int level);

        void PopAndForward(object id, INavigationParameter parameters);

        void PopAndForward(object id, int level, INavigationParameter parameters);
    }
}
