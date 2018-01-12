namespace Smart.Navigation
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class PageAttribute : PageDescriptorAttribute
    {
        public object Id { get; }

        public object Domain { get; }

        public PageAttribute(object id)
        {
            Id = id;
        }

        public PageAttribute(object id, object domain)
        {
            Id = id;
            Domain = domain;
        }

        public override IPageDescriptor CreateDescriptor(Type type)
        {
            return new PageDescriptor(Id, Domain, type);
        }
    }
}
