namespace Smart.Navigation
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class PageDescriptor : IPageDescriptor
    {
        public object Id { get; }

        public object Domain { get; }

        public Type Type { get; }

        public PageDescriptor(object id, object domain, Type type)
        {
            Id = id;
            Domain = domain;
            Type = type;
        }
    }
}
