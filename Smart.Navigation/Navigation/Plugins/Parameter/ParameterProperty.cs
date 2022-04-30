namespace Smart.Navigation.Plugins.Parameter;

public sealed class ParameterProperty
{
    public string Name { get; }

    public Type PropertyType { get; }

    public Func<object?, object?>? Getter { get; }

    public Action<object?, object?>? Setter { get; }

    public ParameterProperty(
        string name,
        Type propertyType,
        Func<object?, object?>? getter,
        Action<object?, object?>? setter)
    {
        Name = name;
        PropertyType = propertyType;
        Getter = getter;
        Setter = setter;
    }
}
