namespace Smart.Navigation.Components
{
    using System;

    public interface IActivator
    {
        object Resolve(Type type);
    }
}
