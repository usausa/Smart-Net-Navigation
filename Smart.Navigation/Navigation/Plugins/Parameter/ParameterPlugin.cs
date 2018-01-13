namespace Smart.Navigation.Plugins.Parameter
{
    using System;
    using System.Collections.Generic;

    using Smart.Navigation.Components;
    using Smart.Reflection;

    public class ParameterPlugin : PluginBase
    {
        private readonly AttributePropertyFactory<ParameterAttribute> attributePropertyFactory;

        private readonly IConverter converter;

        public ParameterPlugin(IAccessorFactory accessorFactory, IConverter converter)
        {
            attributePropertyFactory = new AttributePropertyFactory<ParameterAttribute>(accessorFactory);
            this.converter = converter;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnNavigatedFrom(IPluginContext context, object page, object target)
        {
            context.Save(GetType(), GatherExportParameters(target));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnNavigatingTo(IPluginContext context, object page, object target)
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

            foreach (var property in attributePropertyFactory.GetAttributeProperties(target.GetType()))
            {
                if ((property.Attribute.Direction & Direction.Export) != 0)
                {
                    var name = property.Attribute.Name ?? property.Accessor.Name;
                    parameters.Add(name, property.Accessor.GetValue(target));
                }
            }

            return parameters;
        }

        private void ApplyImportParameters(object target, IDictionary<string, object> parameters)
        {
            foreach (var property in attributePropertyFactory.GetAttributeProperties(target.GetType()))
            {
                if ((property.Attribute.Direction & Direction.Import) != 0)
                {
                    var name = property.Attribute.Name ?? property.Accessor.Name;
                    if (parameters.TryGetValue(name, out var value))
                    {
                        property.Accessor.SetValue(target, converter.Convert(value, property.Accessor.Type));
                    }
                }
            }
        }
    }
}
