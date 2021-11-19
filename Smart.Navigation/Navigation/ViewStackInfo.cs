namespace Smart.Navigation;

public sealed class ViewStackInfo
{
    public ViewDescriptor Descriptor { get; }

    public object View { get; }

    public object? RestoreParameter { get; set; }

    public ViewStackInfo(ViewDescriptor descriptor, object view)
    {
        Descriptor = descriptor;
        View = view;
    }
}
