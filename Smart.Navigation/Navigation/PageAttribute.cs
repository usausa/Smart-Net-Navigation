namespace Smart.Navigation
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class PageAttribute : Attribute
    {
        public object Id { get; }

        public PageAttribute(object id)
        {
            Id = id;
        }
    }
}
