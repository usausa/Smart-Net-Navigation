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

        private readonly Dictionary<Type, ScopeProperty[]> typeProperties = new Dictionary<Type, ScopeProperty[]>();

        private readonly IDelegateFactory delegateFactory;

        private readonly IActivator activator;

        private readonly Dictionary<string, Reference> store = new Dictionary<string, Reference>();

        public ScopePlugin(IDelegateFactory delegateFactory, IActivator activator)
        {
            this.delegateFactory = delegateFactory;
            this.activator = activator;
        }

        private ScopeProperty[] GetTypeProperties(Type type)
        {
            if (!typeProperties.TryGetValue(type, out var properties))
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
                typeProperties[type] = properties;
            }

            return properties;
        }

        public override void OnClose(IPluginContext context, object view, object target)
        {
            if (target is null)
            {
                return;
            }

            foreach (var property in GetTypeProperties(target.GetType()))
            {
                if (store.TryGetValue(property.Name, out var reference))
                {
                    reference.Counter--;
                }
            }

            foreach (var remove in store.Where(x => x.Value.Counter == 0).ToList())
            {
                (remove.Value.Instance as IDisposable)?.Dispose();

                store.Remove(remove.Key);
            }
        }

        public override void OnCreate(IPluginContext context, object view, object target)
        {
            if (target is null)
            {
                return;
            }

            foreach (var property in GetTypeProperties(target.GetType()))
            {
                if (!store.TryGetValue(property.Name, out var reference))
                {
                    reference = new Reference
                    {
                        Instance = activator.Resolve(property.RequestType)
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
