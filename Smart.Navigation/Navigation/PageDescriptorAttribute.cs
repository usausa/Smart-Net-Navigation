namespace Smart.Navigation
{
    using System;

    public abstract class PageDescriptorAttribute : Attribute
    {
        public abstract IPageDescriptor CreateDescriptor(Type type);
    }
}
