namespace Smart.Navigation
{
    using System.Collections.Generic;

    public class NavigationParameter : INavigationParameter
    {
        private readonly Dictionary<string, object> values = new Dictionary<string, object>();

        public T GetValue<T>(string key)
        {
            return (T)values[key];
        }

        public T GetValue<T>()
        {
            return GetValue<T>(typeof(T).Name);
        }

        public T GetValueOrDefault<T>(string key)
        {
            return values.TryGetValue(key, out var value) ? (T)value : default;
        }

        public T GetValueOrDefault<T>()
        {
            return GetValueOrDefault<T>(typeof(T).Name);
        }

        public T GetValueOr<T>(string key, T defaultValue)
        {
            return values.TryGetValue(key, out var value) ? (T)value : defaultValue;
        }

        public T GetValueOr<T>(T defaultValue)
        {
            return GetValueOr(typeof(T).Name, defaultValue);
        }

        public NavigationParameter SetValue<T>(string key, T value)
        {
            values[key] = value;
            return this;
        }

        public NavigationParameter SetValue<T>(T value)
        {
            return SetValue(typeof(T).Name, value);
        }
    }
}
