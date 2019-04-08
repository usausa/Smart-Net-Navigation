namespace Smart.Navigation.Components
{
    using System;

    public sealed class CallbackConverter : IConverter
    {
        private readonly Func<object, Type, object> callback;

        public CallbackConverter(Func<object, Type, object> callback)
        {
            this.callback = callback;
        }

        public object Convert(object value, Type type) => callback(value, type);
    }
}
