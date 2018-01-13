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

        public void UpdateStack(INavigationController controller)
        {
            throw new System.NotImplementedException();
        }

        public void PostProcess(INavigationController controller, object previousPage)
        {
            throw new System.NotImplementedException();
        }
    }
}
