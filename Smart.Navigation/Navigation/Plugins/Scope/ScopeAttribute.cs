namespace Smart.Navigation.Plugins.Scope
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ScopeAttribute : Attribute
    {
        public string Key { get; }

        public Type RequestType { get; }

        public ScopeAttribute()
        {
        }

        public ScopeAttribute(string key)
            : this(key, null)
        {
        }

        public ScopeAttribute(Type requestType)
            : this(null, requestType)
        {
        }

        public ScopeAttribute(string key, Type requestType)
        {
            Key = key;
            RequestType = requestType;
        }
    }
}
