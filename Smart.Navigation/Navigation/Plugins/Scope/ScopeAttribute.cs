namespace Smart.Navigation.Plugins.Scope
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ScopeAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///
        /// </summary>
        public Type ConcreateType { get; }

        /// <summary>
        ///
        /// </summary>
        public ScopeAttribute()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        public ScopeAttribute(string key)
        {
            Key = key;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="concreateType"></param>
        public ScopeAttribute(Type concreateType)
        {
            ConcreateType = concreateType;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="concreateType"></param>
        public ScopeAttribute(string key, Type concreateType)
        {
            Key = key;
            ConcreateType = concreateType;
        }
    }
}
