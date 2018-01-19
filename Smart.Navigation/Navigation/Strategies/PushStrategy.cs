namespace Smart.Navigation.Strategies
{
    using System;

    public sealed class PushStrategy : INavigationStrategy
    {
        private readonly object id;

        private PageDescriptor descriptor;

        public PushStrategy(object id)
        {
            this.id = id;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            if (!controller.Descriptors.TryGetValue(id, out descriptor))
            {
                throw new InvalidOperationException($"Page id is not found in descriptors. id=[{id}]");
            }

            return new StragtegyResult(id, NavigationAttributes.Stacked);
        }

        public object ResolveToPage(INavigationController controller)
        {
            return controller.CreatePage(descriptor.Type);
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            // Stack new
            controller.PageStack.Add(new PageStackInfo(descriptor, toPage));

            controller.OpenPage(toPage);

            // Deactive old
            var count = controller.PageStack.Count;
            if (count > 1)
            {
                var index = count - 2;
                controller.PageStack[index].RestoreParameter = controller.DeactivePage(controller.PageStack[index].Page);
            }
        }
    }
}
