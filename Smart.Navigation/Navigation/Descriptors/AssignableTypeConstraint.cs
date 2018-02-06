namespace Smart.Navigation.Descriptors
{
    using System;

    public sealed class AssignableTypeConstraint : ITypeConstraint
    {
        private readonly Type baseType;

        public AssignableTypeConstraint(Type baseType)
        {
            this.baseType = baseType;
        }

        public bool IsValidType(Type type)
        {
            return baseType.IsAssignableFrom(type);
        }
    }
}
