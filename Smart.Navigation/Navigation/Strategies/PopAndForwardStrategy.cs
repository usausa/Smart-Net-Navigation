namespace Smart.Navigation.Strategies;

public sealed class PopAndForwardStrategy : INavigationStrategy
{
    private readonly bool all;

    private readonly object id;

    private int level;

    private ViewDescriptor descriptor = default!;

    public PopAndForwardStrategy(object id)
    {
        all = true;
        this.id = id;
    }

    public PopAndForwardStrategy(object id, int level)
    {
        all = false;
        this.id = id;
        this.level = level;
    }

    public StrategyResult Initialize(INavigationController controller)
    {
        if (all)
        {
            level = controller.ViewStack.Count - 1;
        }
        else
        {
            if ((level < 1) || (level > controller.ViewStack.Count - 1))
            {
                throw new InvalidOperationException($"Pop level is invalid. level=[{level}], stacked=[{controller.ViewStack.Count}]");
            }
        }

        descriptor = controller.ViewMapper.FindDescriptor(id);

        return new StrategyResult(id, NavigationAttributes.None);
    }

    public object ResolveToView(INavigationController controller)
    {
        return controller.CreateView(descriptor.Type);
    }

    public void UpdateStack(INavigationController controller, object toView)
    {
        // Stack new
        controller.ViewStack.Add(new ViewStackInfo(descriptor, toView));

        controller.OpenView(toView);

        // Remove old
        for (var i = controller.ViewStack.Count - 2; i >= controller.ViewStack.Count - level - 2; i--)
        {
            controller.CloseView(controller.ViewStack[i].View);
        }

        controller.ViewStack.RemoveRange(controller.ViewStack.Count - level - 2, level + 1);
    }
}
