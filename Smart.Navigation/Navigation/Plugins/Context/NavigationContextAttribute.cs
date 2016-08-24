namespace Smart.Navigation.Plugins.Context
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class NavigationContextAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///
        /// </summary>
        public Type Context { get; }

        /// <summary>
        ///
        /// </summary>
        public NavigationContextAttribute()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        public NavigationContextAttribute(string key)
        {
            Key = key;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public NavigationContextAttribute(Type context)
        {
            Context = context;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="context"></param>
        public NavigationContextAttribute(string key, Type context)
        {
            Key = key;
            Context = context;
        }
    }
}
