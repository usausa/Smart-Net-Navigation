namespace Smart.Navigation.Strategies
{
    using System;

    public class PushStrategy : INavigationStrategy
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
                throw new ArgumentException($"{id} is not found in descriptors.");
            }

            return new StragtegyResult(id, NavigationAttribute.Stacked);
        }

        public object ResolveToPage(INavigationController controller)
        {
            throw new NotImplementedException();
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            throw new NotImplementedException();
        }
    }
}
