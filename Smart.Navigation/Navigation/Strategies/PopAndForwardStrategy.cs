namespace Smart.Navigation.Strategies
{
    using System;

    public class PopAndForwardStrategy : INavigationStrategy
    {
        private readonly object id;

        private readonly int level;

        private PageDescriptor descriptor;

        public PopAndForwardStrategy(object id, int level)
        {
            this.id = id;
            this.level = level;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            if (!controller.Descriptors.TryGetValue(id, out descriptor))
            {
                throw new InvalidOperationException($"Page id is not found in descriptors. id=[{id}]");
            }

            if ((level < 1) || (level > controller.PageStack.Count))
            {
                throw new InvalidOperationException($"Pop level is invalid. level=[{level}], stacked=[{controller.PageStack.Count}]");
            }

            return new StragtegyResult(id, NavigationAttribute.None);
        }

        public object ResolveToPage(INavigationController controller)
        {
            return controller.CreatePage(descriptor.Type);
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            throw new NotImplementedException();
        }
    }
}
