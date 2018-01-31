namespace Smart.Navigation
{
    public class DirectContainerResolver : IContainerResolver
    {
        private readonly object container;

        public DirectContainerResolver(object container)
        {
            this.container = container;
        }

        public object Resolve()
        {
            return container;
        }
    }
}
