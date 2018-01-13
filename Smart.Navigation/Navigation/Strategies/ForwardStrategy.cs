namespace Smart.Navigation.Strategies
{
    using System;

    public class ForwardStrategy : INavigationStrategy
    {
        private readonly object id;

        private PageDescriptor descriptor;

        public ForwardStrategy(object id)
        {
            this.id = id;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            if (!controller.Descriptors.TryGetValue(id, out descriptor))
            {
                throw new ArgumentException($"{id} is not found in descriptors.");
            }

            return new StragtegyResult(id, NavigationAttribute.None);
        }

        public object ResolveToPage(INavigationController controller)
        {
            return controller.CreatePage(descriptor.Type);
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            // Stack new
            controller.StackManager.Stacked.Add(new PageStack(descriptor, toPage));

            controller.OpenPage(toPage);

            // Remove old
            var count = controller.StackManager.Stacked.Count;
            if (count > 1)
            {
                controller.ClosePage(controller.StackManager.Stacked[count - 2]);

                controller.StackManager.Stacked.RemoveAt(count - 2);
            }
        }
    }
}
