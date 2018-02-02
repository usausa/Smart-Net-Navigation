namespace Smart.Navigation
{
    using System;

    public interface IViewMapper
    {
        void Add(object id, Type type);

        bool TryGetValue(object id, out ViewDescriptor descriptor);
    }
}
