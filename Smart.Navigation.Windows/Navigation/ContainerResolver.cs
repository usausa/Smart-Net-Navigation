namespace Smart.Navigation;

using System.Windows.Controls;

public sealed class ContainerResolver : IContainerResolver, IUpdateContainer
{
    public Grid? Container { get; private set; }

    public ContainerResolver()
    {
    }

    public ContainerResolver(Grid container)
    {
        Container = container;
    }

    void IUpdateContainer.Attach(Grid? container)
    {
        Container = container;
    }
}
