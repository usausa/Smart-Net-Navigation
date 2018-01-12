namespace Smart.Navigation.Strategies
{
    using System;

    public class ForwardStrategy : INavigationStrategy
    {
        private readonly object id;

        public object ToId => id;

        public NavigationAttribute Attribute => NavigationAttribute.None;

        public ForwardStrategy(object id)
        {
            this.id = id;
        }

        public void Process(INavigationController controller)
        {
            // TODO
            if (id == null)
            {
                throw new InvalidOperationException();
            }

            throw new System.NotImplementedException();
        }
    }
}
