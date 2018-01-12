namespace Smart.Navigation
{
    using System;

    public class PageDescriptor
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
