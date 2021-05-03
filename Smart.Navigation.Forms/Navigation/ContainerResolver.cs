namespace Smart.Navigation
{
    using Xamarin.Forms;

    public sealed class ContainerResolver : IContainerResolver, IUpdateContainer
    {
        public AbsoluteLayout? Container { get; private set; }

        public ContainerResolver()
        {
        }

        public ContainerResolver(AbsoluteLayout container)
        {
            Container = container;
        }

        void IUpdateContainer.Attach(AbsoluteLayout? container)
        {
            Container = container;
        }
    }
}
