namespace Smart.Navigation.Strategies
{
    using System;

    public sealed class PopStrategy : INavigationStrategy
    {
        private readonly int level;

        public PopStrategy(int level)
        {
            this.level = level;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            if ((level < 1) || (level > controller.PageStack.Count - 1))
            {
                throw new InvalidOperationException($"Pop level is invalid. level=[{level}], stacked=[{controller.PageStack.Count}]");
            }

            var toStackInfo = controller.PageStack[controller.PageStack.Count - level - 1];
            return new StragtegyResult(toStackInfo.Descriptor.Id, NavigationAttributes.Restore);
        }

        public object ResolveToPage(INavigationController controller)
        {
            var toStackInfo = controller.PageStack[controller.PageStack.Count - level - 1];
            return toStackInfo.Page;
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            // Remove old
            for (var i = controller.PageStack.Count - 1; i > controller.PageStack.Count - level - 1; i--)
            {
                controller.ClosePage(controller.PageStack[i].Page);
            }

            controller.PageStack.RemoveRange(controller.PageStack.Count - level, level);

            // Activate restored
            var stack = controller.PageStack[controller.PageStack.Count - 1];
            controller.ActivePage(stack.Page, stack.RestoreParameter);
            stack.RestoreParameter = null;
        }
    }
}
