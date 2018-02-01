namespace Smart.Navigation
{
    using System;

    public interface IDescriptorResolver
    {
        void Add(object id, Type type);

        bool TryGetValue(object id, out ViewDescriptor descriptor);
    }
}
