namespace Smart.Navigation;

using System.Windows.Controls;

public interface IContainerResolver
{
    Canvas? Container { get; }
}
