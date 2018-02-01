namespace Smart.Navigation
{
    using System.Windows.Controls;

    public class ContainerResolver : IContainerResolver, IUpdateContainer
    {
        public ContentControl Container { get; private set; }

        public ContainerResolver()
        {
        }

        public ContainerResolver(ContentControl container)
        {
            Container = container;
        }

        void IUpdateContainer.Attach(ContentControl container)
        {
            Container = container;
        }
    }
}
