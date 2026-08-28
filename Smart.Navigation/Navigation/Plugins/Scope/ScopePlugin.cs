namespace Smart.Navigation.Plugins.Scope;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Smart.Navigation.Components;
using Smart.Reflection;

[RequiresUnreferencedCode("ScopePlugin uses reflection to scan properties with [Scope] attribute. This may not work with trimming.")]
[RequiresDynamicCode("ScopePlugin uses dynamic delegate creation which may not work in AOT environments.")]
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

    private readonly Dictionary<Type, ScopeProperty[]> typeProperties = [];

    private readonly IDelegateFactory delegateFactory;

    private readonly IActivator activator;

    private readonly Dictionary<(string Name, Type RequestType), Reference> references = [];

    public ScopePlugin(IDelegateFactory delegateFactory, IActivator activator)
    {
        this.delegateFactory = delegateFactory;
        this.activator = activator;
    }

    private ScopeProperty[] GetTypeProperties(Type type)
    {
        if (!typeProperties.TryGetValue(type, out var properties))
        {
#pragma warning disable IDE0028
            properties = type.GetProperties()
                .Select(static x => new
                {
                    Property = x,
                    Attribute = x.GetCustomAttributes().OfType<IScopeRequest>().FirstOrDefault()
                })
                .Where(static x => x.Attribute is not null)
                .Select(x =>
                {
                    var requestType = x.Attribute!.RequestType ?? delegateFactory.GetExtendedPropertyType(x.Property);
                    if (requestType.IsInterface || requestType.IsAbstract)
                    {
                        throw new InvalidOperationException($"Scope request type must be a concrete class. Use [Scope(typeof(Implementation))] for an interface typed property. type=[{type.FullName}], property=[{x.Property.Name}], requestType=[{requestType.FullName}]");
                    }

                    return new ScopeProperty(
                        x.Attribute.Name ?? x.Property.Name,
                        requestType,
                        delegateFactory.CreateSetter(x.Property, true)!);
                })
                .ToArray();
#pragma warning restore IDE0028
            typeProperties[type] = properties;
        }

        return properties;
    }

    public override void OnClose(IPluginContext pluginContext, object view, object? target)
    {
        if (target is null)
        {
            return;
        }

        foreach (var property in GetTypeProperties(target.GetType()))
        {
            if (references.TryGetValue((property.Name, property.RequestType), out var reference))
            {
                reference.Counter--;
            }
        }

        foreach (var remove in references.Where(static x => x.Value.Counter == 0).ToList())
        {
            (remove.Value.Instance as IDisposable)?.Dispose();

            references.Remove(remove.Key);
        }
    }

    public override void OnCreate(IPluginContext pluginContext, object view, object? target)
    {
        if (target is null)
        {
            return;
        }

        foreach (var property in GetTypeProperties(target.GetType()))
        {
            var key = (property.Name, property.RequestType);
            if (!references.TryGetValue(key, out var reference))
            {
                reference = new Reference(activator.Create(property.RequestType));

                (reference.Instance as IInitializable)?.Initialize();

                references[key] = reference;
            }

            reference.Counter++;

            property.Setter(target, reference.Instance);
        }
    }
}
