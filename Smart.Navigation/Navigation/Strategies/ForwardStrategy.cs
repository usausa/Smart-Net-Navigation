namespace Smart.Navigation.Strategies;

public sealed class ForwardStrategy : IAsyncNavigationStrategy
{
    private readonly object id;

    private ViewDescriptor descriptor = default!;

    public ForwardStrategy(object id)
    {
        this.id = id;
    }

    public StrategyResult Initialize(INavigationController controller)
    {
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
        var count = controller.ViewStack.Count;
        if (count > 1)
        {
            var index = count - 2;

            controller.CloseView(controller.ViewStack[index].View);

            controller.ViewStack.RemoveAt(index);
        }
    }

    public async Task UpdateStackAsync(IAsyncNavigationController controller, object toView, INavigationParameter parameter)
    {
        controller.ViewStack.Add(new ViewStackInfo(descriptor, toView));

        var count = controller.ViewStack.Count;
        var openTask = controller.OpenViewAsync(toView, parameter);

        var closeTask = Task.CompletedTask;
        if (count > 1)
        {
            closeTask = controller.CloseViewAsync(controller.ViewStack[count - 2].View, parameter);
        }

        await Task.WhenAll(openTask, closeTask).ConfigureAwait(true);

        if (count > 1)
        {
            controller.ViewStack.RemoveAt(count - 2);
        }
    }
}
