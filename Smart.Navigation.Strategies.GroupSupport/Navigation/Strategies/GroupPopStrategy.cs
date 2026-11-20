namespace Smart.Navigation.Strategies;

public sealed class GroupPopStrategy : INavigationStrategy
{
    private readonly bool leaveLast;

    private int start;

    private ViewStackInfo restoreStackInfo = default!;

    public GroupPopStrategy(bool leaveLast)
    {
        this.leaveLast = leaveLast;
    }

    public StrategyResult? Initialize(INavigationController controller)
    {
        if (controller.ViewStack.Count == 0)
        {
            throw new InvalidOperationException("View is not stacked.");
        }

        var lastStackInfo = controller.ViewStack[^1];
        var group = GroupAttributeCache.Resolve(lastStackInfo.Descriptor.Type);
        if (group is null)
        {
            throw new InvalidOperationException("Current view is not grouped.");
        }

        start = controller.ViewStack.Count == 1
            ? 0
            : FindLastIndexOfGroup(controller.ViewStack, controller.ViewStack.Count - 2, group.Id);
        if (start == -1)
        {
            start = controller.ViewStack.Count - 1;
        }

        if (leaveLast)
        {
            start = Math.Min(start + 1, controller.ViewStack.Count);
        }

        if (start == controller.ViewStack.Count)
        {
            return null;
        }

        if (start < 1)
        {
            throw new InvalidOperationException($"Pop group is invalid. group=[{group.Id}]");
        }

        restoreStackInfo = controller.ViewStack[start - 1];
        return new StrategyResult(restoreStackInfo.Descriptor.Id, NavigationAttributes.Restore);
    }

    private static int FindLastIndexOfGroup(List<ViewStackInfo> stack, int startIndex, object groupId)
    {
        for (var i = startIndex; i >= 0; i--)
        {
            var groupOfStack = GroupAttributeCache.Resolve(stack[i].Descriptor.Type);
            if ((groupOfStack is not null) && Equals(groupId, groupOfStack.Id))
            {
                return i;
            }
        }

        return -1;
    }

    public object ResolveToView(INavigationController controller)
    {
        return restoreStackInfo.View;
    }

    public void UpdateStack(INavigationController controller, object toView)
    {
        // Remove old
        for (var i = controller.ViewStack.Count - 1; i >= start; i--)
        {
            controller.CloseView(controller.ViewStack[i].View);
        }

        controller.ViewStack.RemoveRange(start, controller.ViewStack.Count - start);

        // Activate restored
        controller.ActivateView(restoreStackInfo.View, restoreStackInfo.RestoreParameter);
        restoreStackInfo.RestoreParameter = null;
    }
}
