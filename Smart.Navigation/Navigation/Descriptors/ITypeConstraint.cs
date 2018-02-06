namespace Smart.Navigation.Descriptors
{
    using System;

    public interface ITypeConstraint
    {
        bool IsValidType(Type type);
    }
}
