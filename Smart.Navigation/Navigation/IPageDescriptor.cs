namespace Smart.Navigation
{
    using System;

    public interface IPageDescriptor
    {
        object Id { get; }

        Type Type { get; }
    }
}
