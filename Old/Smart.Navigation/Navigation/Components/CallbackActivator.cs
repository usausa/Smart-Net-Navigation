namespace Smart.Navigation.Components
{
    using System;

    public class CallbackActivator : IActivator
    {
        private readonly Func<Type, object> callback;

        public CallbackActivator(Func<Type, object> callback)
        {
            this.callback = callback;
        }

        public object Create(Type type)
        {
            return callback(type);
        }
    }
}
