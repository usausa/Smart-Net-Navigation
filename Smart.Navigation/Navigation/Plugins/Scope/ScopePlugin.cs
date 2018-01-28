namespace Smart.Navigation.Plugins.Scope
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Smart.Navigation.Components;
    using Smart.Reflection;

    public sealed class ScopePlugin : PluginBase
    {
        private sealed class Reference
        {
            public object Instance { get; set; }

            public int Counter { get; set; }
        }

        private readonly Dictionary<Type, ScopeProperty[]> typePropertieses = new Dictionary<Type, ScopeProperty[]>();

        private readonly IDelegateFactory delegateFactory;

        private readonly IFactory factory;

        private readonly Dictionary<string, Reference> store = new Dictionary<string, Reference>();

        public ScopePlugin(IDelegateFactory delegateFactory, IFactory factory)
        {
            this.delegateFactory = delegateFactory;
            this.factory = factory;
        }

        private ScopeProperty[] GetTypeProperties(Type type)
        {
            if (!typePropertieses.TryGetValue(type, out var properties))
            {
                properties = type.GetProperties()
                    .Select(x => new
                    {
                        Property = x,
                        Attribute = (ScopeAttribute)x.GetCustomAttribute(typeof(ScopeAttribute))
                    })
                    .Where(x => x.Attribute != null)
                    .Select(x => new ScopeProperty(
                        x.Attribute.Name ?? x.Property.Name,
                        x.Attribute.RequestType ?? delegateFactory.GetExtendedPropertyType(x.Property),
                        delegateFactory.CreateSetter(x.Property, true)))
                    .ToArray();
                typePropertieses[type] = properties;
            }

            return properties;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnClose(IPluginContext context, object view, object target)
        {
            foreach (var property in GetTypeProperties(target.GetType()))
            {
                if (store.TryGetValue(property.Name, out var reference))
                {
                    reference.Counter--;
                    if (reference.Counter > 0)
                    {
                        continue;
                    }

                    (reference.Instance as IDisposable)?.Dispose();

                    store.Remove(property.Name);
                }
            }
        }

        public override void OnCreate(IPluginContext context, object view, object target)
        {
            foreach (var property in GetTypeProperties(target.GetType()))
            {
                if (!store.TryGetValue(property.Name, out var reference))
                {
                    reference = new Reference
                    {
                        Instance = factory.Create(property.RequestType)
                    };

                    (reference.Instance as IInitializable)?.Initialize();

                    store[property.Name] = reference;
                }

                reference.Counter++;

                property.Setter(target, reference.Instance);
            }
        }
    }
}
