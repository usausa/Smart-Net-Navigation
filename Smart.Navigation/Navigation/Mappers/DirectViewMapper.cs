namespace Smart.Navigation.Mappers
{
    using System;
    using System.Collections.Generic;

    public sealed class DirectViewMapper : IViewMapper
    {
        private readonly Dictionary<Type, ViewDescriptor> descriptors = new();

        private readonly ITypeConstraint constraint;

        public DirectViewMapper(ITypeConstraint constraint)
        {
            this.constraint = constraint;
        }

        public ViewDescriptor FindDescriptor(object id)
        {
            if (id is Type type && constraint.IsValidType(type))
            {
                if (!descriptors.TryGetValue(type, out var descriptor))
                {
                    descriptor = new ViewDescriptor(type, type);
                    descriptors[type] = descriptor;
                }

                return descriptor;
            }

            throw new InvalidOperationException($"View id is invalid id type. id=[{id}]");
        }

        public void CurrentUpdated(object? id)
        {
        }
    }
}
