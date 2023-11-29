namespace Smart.Navigation.Mappers;

public sealed class IdViewMapper : IViewMapper, IIdViewRegister
{
    private readonly Dictionary<object, ViewDescriptor> descriptors = [];

    private readonly ITypeConstraint constraint;

    public IdViewMapper(IdViewMapperOptions options, ITypeConstraint constraint)
    {
        this.constraint = constraint;

        options.SetupAction(this);
    }

    public void Register(object id, Type type)
    {
        if (!constraint.IsValidType(type))
        {
            throw new ArgumentException($"Type is invalid. type=[{type.FullName}]", nameof(type));
        }

        descriptors[id] = new ViewDescriptor(id, type);
    }

    public ViewDescriptor FindDescriptor(object id)
    {
        if (!descriptors.TryGetValue(id, out var descriptor))
        {
            throw new InvalidOperationException($"View id is not found in descriptors. id=[{id}]");
        }

        return descriptor;
    }

    public void CurrentUpdated(object? id)
    {
    }
}
