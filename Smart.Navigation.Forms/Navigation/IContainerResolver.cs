namespace Smart.Navigation
{
    using Xamarin.Forms;

    public interface IContainerResolver
    {
        AbsoluteLayout? Container { get; }
    }
}
