namespace Smart.Navigation.Plugins.Scope
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.Navigation.Components;

    public class ScopePlugin : PluginBase
    {
        private class Reference
        {
            public object Instance { get; set; }

            public int Counter { get; set; }
        }

        private readonly AttributePropertyFactory<ScopeAttribute> factory = new AttributePropertyFactory<ScopeAttribute>();

        private readonly Dictionary<string, Reference> store = new Dictionary<string, Reference>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnClose(IPluginContext context, object page, object target)
        {
            foreach (var property in factory.GetAttributeProperties(target.GetType()))
            {
                var key = property.Attribute.Key ?? property.Accessor.Type.FullName;

                Reference reference;
                if (store.TryGetValue(key, out reference))
                {
                    reference.Counter--;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnCreate(IPluginContext context, object page, object target)
        {
            var activator = context.Components.Get<IActivator>();

            foreach (var property in factory.GetAttributeProperties(target.GetType()))
            {
                var key = property.Attribute.Key ?? property.Accessor.Type.FullName;

                Reference reference;
                if (!store.TryGetValue(key, out reference))
                {
                    reference = new Reference
                    {
                        Instance = activator.Create(property.Attribute.ConcreateType ?? property.Accessor.Type)
                    };

                    (reference.Instance as IScopeInitializable)?.Initialize();

                    store[key] = reference;
                }

                reference.Counter++;

                property.Accessor.SetValue(target, reference.Instance);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnPostProcess(IPluginContext context)
        {
            var removes = store.Where(_ => _.Value.Counter == 0).ToList();
            foreach (var remove in removes)
            {
                store.Remove(remove.Key);

                (remove.Value.Instance as IDisposable)?.Dispose();
            }
        }
    }
}
