namespace Smart.Navigation.Plugins.Parameter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class ParameterPlugin : PluginBase
    {
        private readonly AttributePropertyFactory<ParameterAttribute> factory = new AttributePropertyFactory<ParameterAttribute>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnNavigatedFrom(PluginContext context, object page, object target)
        {
            context.Save(GetType(), GatherExportParameters(target));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public override void OnNavigatedTo(PluginContext context, object page, object target)
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

            foreach (var property in factory.GetAttributeProperties(target.GetType()))
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
            foreach (var property in factory.GetAttributeProperties(target.GetType()))
            {
                if ((property.Attribute.Direction & Direction.Import) != 0)
                {
                    var name = property.Attribute.Name ?? property.Accessor.Name;
                    object value;
                    if (parameters.TryGetValue(name, out value))
                    {
                        property.Accessor.SetValue(target, Convert.ChangeType(value, property.Accessor.Type, CultureInfo.InvariantCulture));
                    }
                }
            }
        }
    }
}
