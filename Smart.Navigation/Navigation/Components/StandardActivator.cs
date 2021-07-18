namespace Smart.Navigation.Components
{
    using System;

    public sealed class StandardActivator : IActivator
    {
        public object Activate(Type type)
        {
            return Activator.CreateInstance(type)!;
        }
    }
}
