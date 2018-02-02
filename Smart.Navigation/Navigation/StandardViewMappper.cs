namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;

    public class StandardViewMappper : IViewMapper
    {
        private readonly Dictionary<object, ViewDescriptor> descriptors = new Dictionary<object, ViewDescriptor>();

        public void Add(object id, Type type)
        {
            descriptors[id] = new ViewDescriptor(id, type);
        }

        public bool TryGetValue(object id, out ViewDescriptor descriptor)
        {
            return descriptors.TryGetValue(id, out descriptor);
        }
    }
}
