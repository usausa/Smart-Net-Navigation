namespace Smart.Navigation.Plugins
{
    using System;

    using Smart.Reflection;

    public class AttributeProperty<T>
        where T : Attribute
    {
        public T Attribute { get; }

        public IAccessor Accessor { get; }

        public AttributeProperty(T attribute, IAccessor accessor)
        {
            Attribute = attribute;
            Accessor = accessor;
        }
    }
}
