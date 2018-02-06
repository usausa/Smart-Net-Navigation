namespace Smart.Navigation.Descriptors
{
    using System;
    using System.Collections.Generic;

    public sealed class DirectViewMapper : IViewMapper
    {
        private readonly Dictionary<Type, ViewDescriptor> descriptors = new Dictionary<Type, ViewDescriptor>();

        private readonly ITypeConstraint constraint;

        public DirectViewMapper()
            : this(null)
        {
        }

        public DirectViewMapper(ITypeConstraint constraint)
        {
            this.constraint = constraint;
        }

        public ViewDescriptor FindDescriptor(object id)
        {
            if (id is Type type &&
                (constraint?.IsValidType(type) ?? true))
            {
                if (!descriptors.TryGetValue(type, out var descriptor))
                {
                    descriptor = new ViewDescriptor(type, type);
                    descriptors[type] = descriptor;
                }

                return descriptor;
            }

            throw new InvalidOperationException($"View id is invalid view type. id=[{id}]");
        }

        public void Updated(object id)
        {
        }
    }
}
