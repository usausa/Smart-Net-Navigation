namespace Smart.Navigation.Plugins.Scope
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Smart.Reflection;

    public sealed class ScopePlugin : PluginBase
    {
        private sealed class Reference
        {
            public object? Instance { get; }

            public int Counter { get; set; }

            public Reference(object? instance)
            {
                Instance = instance;
            }
        }

        private readonly Dictionary<Type, ScopeProperty[]> typeProperties = new();

        private readonly IDelegateFactory delegateFactory;

        private readonly IServiceProvider serviceProvider;

        private readonly Dictionary<string, Reference> references = new();

        public ScopePlugin(IDelegateFactory delegateFactory, IServiceProvider serviceProvider)
        {
            this.delegateFactory = delegateFactory;
            this.serviceProvider = serviceProvider;
        }

        private ScopeProperty[] GetTypeProperties(Type type)
        {
            if (!typeProperties.TryGetValue(type, out var properties))
            {
                properties = type.GetProperties()
                    .Select(x => new
                    {
                        Property = x,
                        Attribute = (ScopeAttribute?)x.GetCustomAttribute(typeof(ScopeAttribute))
                    })
                    .Where(x => x.Attribute is not null)
                    .Select(x => new ScopeProperty(
                        x.Attribute!.Name ?? x.Property.Name,
                        x.Attribute.RequestType ?? delegateFactory.GetExtendedPropertyType(x.Property),
                        delegateFactory.CreateSetter(x.Property, true)!))
                    .ToArray();
                typeProperties[type] = properties;
            }

            return properties;
        }

        public override void OnClose(IPluginContext pluginContext, object view, object target)
        {
            foreach (var property in GetTypeProperties(target.GetType()))
            {
                if (references.TryGetValue(property.Name, out var reference))
                {
                    reference.Counter--;
                }
            }

            foreach (var remove in references.Where(x => x.Value.Counter == 0).ToList())
            {
                (remove.Value.Instance as IDisposable)?.Dispose();

                references.Remove(remove.Key);
            }
        }

        public override void OnCreate(IPluginContext pluginContext, object view, object target)
        {
            foreach (var property in GetTypeProperties(target.GetType()))
            {
                if (!references.TryGetValue(property.Name, out var reference))
                {
                    reference = new Reference(serviceProvider.GetService(property.RequestType));

                    (reference.Instance as IInitializable)?.Initialize();

                    references[property.Name] = reference;
                }

                reference.Counter++;

                property.Setter(target, reference.Instance);
            }
        }
    }
}
