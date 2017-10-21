namespace Smart.Navigation.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Smart.Reflection;

    public class AttributePropertyFactory<T>
        where T : Attribute
    {
        private readonly Dictionary<Type, AttributeProperty<T>[]> cache = new Dictionary<Type, AttributeProperty<T>[]>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public AttributeProperty<T>[] GetAttributeProperties(Type type)
        {
            AttributeProperty<T>[] properties;
            if (cache.TryGetValue(type, out properties))
            {
                return properties;
            }

            properties = type.GetProperties()
                .SelectMany(
                    pi => pi.GetCustomAttributes(typeof(T)),
                    (pi, attr) => new AttributeProperty<T>((T)attr, pi.ToAccessor()))
                .ToArray();
            cache[type] = properties;

            return properties;
        }
    }
}
