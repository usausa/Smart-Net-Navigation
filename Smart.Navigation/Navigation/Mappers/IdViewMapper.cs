namespace Smart.Navigation.Mappers
{
    using System;
    using System.Collections.Generic;

    public sealed class IdViewMapper : IViewMapper, IViewRegister
    {
        private readonly Dictionary<object, ViewDescriptor> descriptors = new Dictionary<object, ViewDescriptor>();

        private readonly ITypeConstraint constraint;

        public IdViewMapper()
            : this(null)
        {
        }

        public IdViewMapper(ITypeConstraint constraint)
        {
            this.constraint = constraint;
        }

        public void Register(object id, Type type)
        {
            if (!constraint?.IsValidType(type) ?? false)
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

        public void CurrentUpdated(object id)
        {
        }
    }
}
