namespace Smart.Navigation.Plugins
{
    using System;
    using System.Collections.Generic;

    public sealed class PluginContext : IPluginContext
    {
        private Dictionary<Type, object> store;

        private void Prepare()
        {
            if (store is null)
            {
                store = new Dictionary<Type, object>();
            }
        }

        public void Save<T>(Type type, T value)
        {
            Prepare();
            store[type] = value;
        }

        public T Load<T>(Type type)
        {
            Prepare();
            return (T)store[type];
        }

        public T LoadOr<T>(Type type, T defaultValue)
        {
            if (store is null)
            {
                return defaultValue;
            }

            Prepare();

            return store.TryGetValue(type, out var value) ? (T)value : defaultValue;
        }

        public T LoadOr<T>(Type type, Func<T> defaultValueFactory)
        {
            if (defaultValueFactory is null)
            {
                throw new ArgumentNullException(nameof(defaultValueFactory));
            }

            if (store is null)
            {
                return defaultValueFactory();
            }

            Prepare();

            return store.TryGetValue(type, out var value) ? (T)value : defaultValueFactory();
        }
    }
}
