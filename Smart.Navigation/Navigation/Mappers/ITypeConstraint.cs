namespace Smart.Navigation.Mappers;

public interface ITypeConstraint
{
    bool IsValidType(Type type);
}
