namespace Smart.Navigation
{
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class NavigationParameter : INavigationParameter
    {
        private readonly Dictionary<string, object> values = new Dictionary<string, object>();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            return (T)values[key];
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return GetValue<T>(typeof(T).Name);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string key)
        {
            object value;
            return values.TryGetValue(key, out value) ? (T)value : default(T);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValueOrDefault<T>()
        {
            return GetValueOrDefault<T>(typeof(T).Name);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOr<T>(string key, T defaultValue)
        {
            object value;
            return values.TryGetValue(key, out value) ? (T)value : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOr<T>(T defaultValue)
        {
            return GetValueOr(typeof(T).Name, defaultValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public NavigationParameter SetValue<T>(string key, T value)
        {
            values[key] = value;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public NavigationParameter SetValue<T>(T value)
        {
            return SetValue(typeof(T).Name, value);
        }
    }
}
