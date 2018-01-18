namespace Smart.Navigation.Strategies
{
    using System;
    using System.Reflection;

    public sealed class GroupPopStragety : INavigationStrategy
    {
        private readonly bool leaveLast;

        private int start;

        private PageStackInfo restoreStackInfo;

        public GroupPopStragety(bool leaveLast)
        {
            this.leaveLast = leaveLast;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            if (controller.PageStack.Count == 0)
            {
                throw new InvalidOperationException("Page is not stacked.");
            }

            var lastStackInfo = controller.PageStack[controller.PageStack.Count - 1];
            var group = lastStackInfo.Descriptor.Type.GetCustomAttribute<GroupAttribute>();
            if (group == null)
            {
                throw new InvalidOperationException("Current page is not grouped.");
            }

            start = controller.PageStack.Count == 1
                ? 0
                : controller.PageStack.FindLastIndex(controller.PageStack.Count - 2, stack =>
                {
                    var groupOfStack = stack.Descriptor.Type.GetCustomAttribute<GroupAttribute>();
                    return (groupOfStack != null) && Equals(group.Id, groupOfStack.Id);
                });
            if (start == -1)
            {
                start = controller.PageStack.Count - 1;
            }

            if (leaveLast)
            {
                start = Math.Min(start + 1, controller.PageStack.Count);
            }

            if (start == controller.PageStack.Count)
            {
                return null;
            }

            if (start < 1)
            {
                throw new InvalidOperationException($"Pop group is invalid. group=[{group.Id}]");
            }

            restoreStackInfo = controller.PageStack[start - 1];
            return new StragtegyResult(restoreStackInfo.Descriptor.Id, NavigationAttributes.Restore);
        }

        public object ResolveToPage(INavigationController controller)
        {
            return restoreStackInfo.Page;
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            // Remove old
            for (var i = controller.PageStack.Count - 1; i >= start; i--)
            {
                controller.ClosePage(controller.PageStack[i].Page);
            }

            controller.PageStack.RemoveRange(start, controller.PageStack.Count - start);

            // Activate restored
            controller.ActivePage(restoreStackInfo.Page, restoreStackInfo.RestoreParameter);
            restoreStackInfo.RestoreParameter = null;
        }
    }
}
