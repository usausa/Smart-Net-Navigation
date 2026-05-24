namespace Smart.Navigation.Mappers;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[RequiresDynamicCode("PathViewMapperOptions uses Assembly.GetType for dynamic type resolution which is not AOT compatible.")]
[RequiresUnreferencedCode("PathViewMapperOptions uses Assembly.GetType which may not work with trimming.")]
public sealed class PathViewMapperOptions
{
    private readonly List<Assembly> assemblies = [];

    public string Root { get; set; } = string.Empty;

    public string Suffix { get; set; } = string.Empty;

    public void AddAssembly(Assembly assembly)
    {
        assemblies.Add(assembly);
    }

    public Type? FindType(string typeName)
    {
        for (var i = 0; i < assemblies.Count; i++)
        {
            var type = assemblies[i].GetType(typeName);
            if (type is not null)
            {
                return type;
            }
        }

        return null;
    }
}
