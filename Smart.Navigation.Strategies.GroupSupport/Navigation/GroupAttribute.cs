namespace Smart.Navigation;

[AttributeUsage(AttributeTargets.Class)]
public sealed class GroupAttribute : Attribute
{
    public object Id { get; }

    public GroupAttribute(object id)
    {
        Id = id;
    }
}
