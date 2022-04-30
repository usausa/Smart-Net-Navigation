namespace Smart.Navigation.Plugins.Parameter;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ParameterAttribute : Attribute
{
    public Directions Direction { get; }

    public string? Name { get; }

    public ParameterAttribute()
        : this(Directions.Both)
    {
    }

    public ParameterAttribute(string name)
        : this(Directions.Both, name)
    {
    }

    public ParameterAttribute(Directions direction)
    {
        Direction = direction;
    }

    public ParameterAttribute(Directions direction, string name)
    {
        Direction = direction;
        Name = name;
    }
}
