namespace Smart.Navigation.Strategies
{
    using System;
    using System.Reflection;

    public sealed class GroupPopStragety : INavigationStrategy
    {
        private readonly bool leaveLast;

        private int index;

        private PageStackInfo restoreStackInfo;

        public GroupPopStragety(bool leaveLast)
        {
            this.leaveLast = leaveLast;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            if (controller.PageStack.Count == 0)
            {
                return null;
            }

            var lastStackInfo = controller.PageStack[controller.PageStack.Count - 1];
            var group = lastStackInfo.Descriptor.Type.GetCustomAttribute<GroupAttribute>();
            if (group == null)
            {
                return null;
            }

            index = controller.PageStack
                .FindLastIndex(stack =>
                {
                    var groupOfStack = stack.Descriptor.Type.GetCustomAttribute<GroupAttribute>();
                    return (groupOfStack != null) && Equals(group.Id, groupOfStack.Id);
                });
            if (leaveLast)
            {
                index = Math.Min(index + 1, controller.PageStack.Count - 1);
            }

            if (index < 1)
            {
                throw new InvalidOperationException($"Pop group is invalid. group=[{group.Id}]");
            }

            restoreStackInfo = controller.PageStack[index - 1];
            return new StragtegyResult(restoreStackInfo.Descriptor.Id, NavigationAttributes.Restore);
        }

        public object ResolveToPage(INavigationController controller)
        {
            return restoreStackInfo.Page;
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            // Remove old
            for (var i = controller.PageStack.Count - 1; i >= index; i--)
            {
                controller.ClosePage(controller.PageStack[i].Page);
            }

            // Activate restored
            controller.ActivePage(restoreStackInfo.Page, restoreStackInfo.RestoreParameter);
            restoreStackInfo.RestoreParameter = null;
        }
    }
}
