namespace Smart.Navigation.Descriptors
{
    using System;

    public interface IViewMapper
    {
        ViewDescriptor FindDescriptor(object id);
    }
}
