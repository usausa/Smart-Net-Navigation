namespace Smart.Navigation.Plugins.Scope
{
    using System;
    using System.Collections.Generic;

    using Smart.Navigation.Components;
    using Smart.Reflection;

    public sealed class ScopePlugin : PluginBase
    {
        private sealed class Reference
        {
            public object Instance { get; set; }

            public int Counter { get; set; }
        }

        private readonly AttributePropertyFactory<ScopeAttribute> attributePropertyFactory;

        private readonly IFactory factory;

        private readonly Dictionary<string, Reference> store = new Dictionary<string, Reference>();

        public ScopePlugin(IAccessorFactory accessorFactory, IFactory factory)
        {
            attributePropertyFactory = new AttributePropertyFactory<ScopeAttribute>(accessorFactory);
            this.factory = factory;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnClose(IPluginContext context, object page, object target)
        {
            foreach (var property in attributePropertyFactory.GetAttributeProperties(target.GetType()))
            {
                var key = property.Attribute.Name ?? property.Accessor.Name;

                if (store.TryGetValue(key, out var reference))
                {
                    reference.Counter--;
                    if (reference.Counter > 0)
                    {
                        continue;
                    }

                    (reference.Instance as IDisposable)?.Dispose();

                    store.Remove(key);
                }
            }
        }

        public override void OnCreate(IPluginContext context, object page, object target)
        {
            foreach (var property in attributePropertyFactory.GetAttributeProperties(target.GetType()))
            {
                var key = property.Attribute.Name ?? property.Accessor.Name;

                if (!store.TryGetValue(key, out var reference))
                {
                    reference = new Reference
                    {
                        Instance = factory.Create(property.Attribute.RequestType ?? property.Accessor.Type)
                    };

                    (reference.Instance as IInitializable)?.Initialize();

                    store[key] = reference;
                }

                reference.Counter++;

                property.Accessor.SetValue(target, reference.Instance);
            }
        }
    }
}
