namespace Smart.Navigation
{
    using System;

    public sealed class PageDescriptor
    {
        public object Id { get; }

        public Type Type { get; }

        public PageDescriptor(object id, Type type)
        {
            Id = id;
            Type = type;
        }
    }
}
