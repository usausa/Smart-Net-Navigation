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

            var toInfo = controller.PageStack[controller.PageStack.Count - level - 1];
            return new StragtegyResult(toInfo.Descriptor.Id, NavigationAttribute.Restore);
        }

        public object ResolveToPage(INavigationController controller)
        {
            var toInfo = controller.PageStack[controller.PageStack.Count - level - 1];
            return toInfo.Page;
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            for (var i = controller.PageStack.Count - 1; i > controller.PageStack.Count - level - 1; i--)
            {
                controller.ClosePage(controller.PageStack[i]);
            }

            controller.PageStack.RemoveRange(controller.PageStack.Count - level, level);

            var stack = controller.PageStack[controller.PageStack.Count - 1];
            controller.ActivaPage(stack.Page, stack.RestoreParameter);
            stack.RestoreParameter = null;
        }
    }
}
