namespace Smart.Navigation.Components
{
    using System;

    public interface IActivator
    {
        object Activate(Type type);
    }
}
