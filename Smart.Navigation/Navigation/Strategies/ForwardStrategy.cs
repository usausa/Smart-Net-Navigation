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

        public void UpdateStack(INavigationController controller)
        {
            var count = controller.StackManager.Stacked.Count;
            if (count > 0)
            {
                controller.ClosePage(controller.StackManager.Stacked[count - 1]);

                controller.StackManager.Stacked.RemoveAt(count - 1);
            }

            var page = controller.CreatePage(descriptor.Type);
            controller.StackManager.Stacked.Add(new PageStack(descriptor, page));
        }

        public void PostProcess(INavigationController controller, object previousPage)
        {
            if (previousPage != null)
            {
                controller.ClosePage(previousPage);
            }
        }
    }
}
