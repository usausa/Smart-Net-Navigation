namespace Smart.Navigation.Plugins.Parameter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Smart.Navigation.Components;
    using Smart.Reflection;

    public class ParameterPlugin : PluginBase
    {
        private readonly Dictionary<Type, ParameterProperty[]> typePropertieses = new Dictionary<Type, ParameterProperty[]>();

        private readonly IDelegateFactory delegateFactory;

        private readonly IConverter converter;

        public ParameterPlugin(IDelegateFactory delegateFactory, IConverter converter)
        {
            this.delegateFactory = delegateFactory;
            this.converter = converter;
        }

        private ParameterProperty[] GetTypeProperties(Type type)
        {
            if (!typePropertieses.TryGetValue(type, out var properties))
            {
                properties = type.GetProperties()
                    .Select(x => new
                    {
                        Property = x,
                        Attribute = (ParameterAttribute)x.GetCustomAttribute(typeof(ParameterAttribute))
                    })
                    .Where(x => x.Attribute != null)
                    .Select(x => new ParameterProperty(
                        x.Attribute.Name ?? x.Property.Name,
                        delegateFactory.GetExtendedPropertyType(x.Property),
                        (x.Attribute.Direction & Directions.Export) != 0 ? delegateFactory.CreateGetter(x.Property, true) : null,
                        (x.Attribute.Direction & Directions.Import) != 0 ? delegateFactory.CreateSetter(x.Property, true) : null))
                    .ToArray();
                typePropertieses[type] = properties;
            }

            return properties;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnNavigatedFrom(IPluginContext context, object view, object target)
        {
            context.Save(GetType(), GatherExportParameters(target));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnNavigatingTo(IPluginContext context, object view, object target)
        {
            var parameters = context.LoadOr(GetType(), default(Dictionary<string, object>));
            if (parameters != null)
            {
                ApplyImportParameters(target, parameters);
            }
        }

        private Dictionary<string, object> GatherExportParameters(object target)
        {
            var parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            foreach (var property in GetTypeProperties(target.GetType()))
            {
                if (property.Getter != null)
                {
                    parameters.Add(property.Name, property.Getter(target));
                }
            }

            return parameters;
        }

        private void ApplyImportParameters(object target, IDictionary<string, object> parameters)
        {
            foreach (var property in GetTypeProperties(target.GetType()))
            {
                if ((property.Setter != null) &&
                    parameters.TryGetValue(property.Name, out var value))
                {
                    property.Setter(target, converter.Convert(value, property.PropertyType));
                }
            }
        }
    }
}
