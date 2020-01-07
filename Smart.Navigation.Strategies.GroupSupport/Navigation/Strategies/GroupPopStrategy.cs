namespace Smart.Navigation.Strategies
{
    using System;
    using System.Reflection;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Strategy")]
    public sealed class GroupPopStrategy : INavigationStrategy
    {
        private readonly bool leaveLast;

        private int start;

        private ViewStackInfo restoreStackInfo;

        public GroupPopStrategy(bool leaveLast)
        {
            this.leaveLast = leaveLast;
        }

        public StrategyResult Initialize(INavigationController controller)
        {
            if (controller.ViewStack.Count == 0)
            {
                throw new InvalidOperationException("View is not stacked.");
            }

            var lastStackInfo = controller.ViewStack[^1];
            var group = lastStackInfo.Descriptor.Type.GetCustomAttribute<GroupAttribute>();
            if (group is null)
            {
                throw new InvalidOperationException("Current view is not grouped.");
            }

            start = controller.ViewStack.Count == 1
                ? 0
                : controller.ViewStack.FindLastIndex(controller.ViewStack.Count - 2, stack =>
                {
                    var groupOfStack = stack.Descriptor.Type.GetCustomAttribute<GroupAttribute>();
                    return (groupOfStack != null) && Equals(group.Id, groupOfStack.Id);
                });
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
}
