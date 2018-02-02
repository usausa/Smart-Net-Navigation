namespace Smart.Navigation.Strategies
{
    using System;

    public sealed class PopAndForwardStrategy : INavigationStrategy
    {
        private readonly object id;

        private readonly int level;

        private ViewDescriptor descriptor;

        public PopAndForwardStrategy(object id, int level)
        {
            this.id = id;
            this.level = level;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            if (!controller.ViewMapper.TryGetValue(id, out descriptor))
            {
                throw new InvalidOperationException($"View id is not found in descriptors. id=[{id}]");
            }

            if ((level < 1) || (level > controller.ViewStack.Count - 1))
            {
                throw new InvalidOperationException($"Pop level is invalid. level=[{level}], stacked=[{controller.ViewStack.Count}]");
            }

            return new StragtegyResult(id, NavigationAttributes.None);
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
}
