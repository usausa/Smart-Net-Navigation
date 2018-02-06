namespace Smart.Navigation.Mappers
{
    using System;

    public interface ITypeConstraint
    {
        bool IsValidType(Type type);
    }
}
