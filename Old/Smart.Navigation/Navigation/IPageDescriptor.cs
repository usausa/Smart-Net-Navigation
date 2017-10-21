namespace Smart.Navigation
{
    using System;

    public interface IPageDescriptor
    {
        object Id { get; }

        object Domain { get; }

        Type Type { get; }
    }
}
