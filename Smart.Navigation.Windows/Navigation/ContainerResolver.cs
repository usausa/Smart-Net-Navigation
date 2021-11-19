namespace Smart.Navigation;

using System.Windows.Controls;

public sealed class ContainerResolver : IContainerResolver, IUpdateContainer
{
    public Canvas? Container { get; private set; }

    public ContainerResolver()
    {
    }

    public ContainerResolver(Canvas container)
    {
        Container = container;
    }

    void IUpdateContainer.Attach(Canvas? container)
    {
        Container = container;
    }
}
