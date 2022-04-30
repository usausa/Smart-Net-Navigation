namespace Smart.Navigation;

public sealed class ViewDescriptor
{
    public object Id { get; }

    public Type Type { get; }

    public ViewDescriptor(object id, Type type)
    {
        Id = id;
        Type = type;
    }
}
