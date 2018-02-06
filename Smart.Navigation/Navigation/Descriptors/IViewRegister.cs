namespace Smart.Navigation.Descriptors
{
    using System;

    public interface IViewRegister
    {
        void Register(object id, Type type);
    }
}
