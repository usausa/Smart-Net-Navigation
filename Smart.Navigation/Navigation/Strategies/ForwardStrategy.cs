namespace Smart.Navigation.Strategies
{
    using System;

    public class ForwardStrategy : INavigationStrategy
    {
        private readonly object id;

        public ForwardStrategy(object id)
        {
            this.id = id;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            // TODO descrpitor check

            return new StragtegyResult(id, NavigationAttribute.None);
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
