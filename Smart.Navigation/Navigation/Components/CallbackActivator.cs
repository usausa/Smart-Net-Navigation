namespace Smart.Navigation.Components
{
    using System;

    public sealed class CallbackActivator : IActivator
    {
        private readonly Func<Type, object> callback;

        public CallbackActivator(Func<Type, object> callback)
        {
            this.callback = callback;
        }

        public object Resolve(Type type) => callback(type);
    }
}
