namespace Smart.Navigation
{
    using Xamarin.Forms;

    public class ContainerResolver : IContainerResolver, IUpdateContainer
    {
        public ContentView Container { get; private set; }

        public ContainerResolver()
        {
        }

        public ContainerResolver(ContentView container)
        {
            Container = container;
        }

        void IUpdateContainer.Attach(ContentView container)
        {
            Container = container;
        }
    }
}
