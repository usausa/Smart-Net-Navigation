namespace Smart.Navigation.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class ViewAttribute : Attribute
{
    public object Id { get; }

    public ViewAttribute(object id)
    {
        Id = id;
    }
}
