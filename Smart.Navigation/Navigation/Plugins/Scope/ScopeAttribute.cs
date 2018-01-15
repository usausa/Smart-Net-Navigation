namespace Smart.Navigation.Plugins.Scope
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ScopeAttribute : Attribute
    {
        public string Name { get; }

        public Type RequestType { get; }

        public ScopeAttribute()
        {
        }

        public ScopeAttribute(string name)
            : this(name, null)
        {
        }

        public ScopeAttribute(Type requestType)
            : this(null, requestType)
        {
        }

        public ScopeAttribute(string name, Type requestType)
        {
            Name = name;
            RequestType = requestType;
        }
    }
}
