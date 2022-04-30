namespace Smart.Navigation.Strategies;

public sealed class PopStrategy : INavigationStrategy
{
    private readonly int level;

    private ViewStackInfo restoreStackInfo = default!;

    public PopStrategy(int level)
    {
        this.level = level;
    }

    public StrategyResult Initialize(INavigationController controller)
    {
        if ((level < 1) || (level > controller.ViewStack.Count - 1))
        {
            throw new InvalidOperationException($"Pop level is invalid. level=[{level}], stacked=[{controller.ViewStack.Count}]");
        }

        restoreStackInfo = controller.ViewStack[controller.ViewStack.Count - level - 1];
        return new StrategyResult(restoreStackInfo.Descriptor.Id, NavigationAttributes.Restore);
    }

    public object ResolveToView(INavigationController controller)
    {
        return restoreStackInfo.View;
    }

    public void UpdateStack(INavigationController controller, object toView)
    {
        // Activate restored
        controller.ActivateView(restoreStackInfo.View, restoreStackInfo.RestoreParameter);
        restoreStackInfo.RestoreParameter = null;

        // Remove old
        for (var i = controller.ViewStack.Count - 1; i > controller.ViewStack.Count - level - 1; i--)
        {
            controller.CloseView(controller.ViewStack[i].View);
        }

        controller.ViewStack.RemoveRange(controller.ViewStack.Count - level, level);
    }
}
