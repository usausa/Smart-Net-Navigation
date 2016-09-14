﻿namespace Smart.Navigation.Components
{
    using System;

    public class CallbackConverter : IConverter
    {
        private readonly Func<object, Type, object> callback;

        public CallbackConverter(Func<object, Type, object> callback)
        {
            this.callback = callback;
        }

        public object Convert(object value, Type type)
        {
            return callback(value, type);
        }
    }
}
