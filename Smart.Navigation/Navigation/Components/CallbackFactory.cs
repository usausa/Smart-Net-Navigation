namespace Smart.Navigation.Components
{
    using System;

    public sealed class CallbackFactory : IFactory
    {
        private readonly Func<Type, object> callback;

        public CallbackFactory(Func<Type, object> callback)
        {
            this.callback = callback;
        }

        public object Create(Type type)
        {
            return callback(type);
        }
    }
}
