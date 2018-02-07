namespace Smart.Navigation.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class PathViewMapperOptions
    {
        private readonly List<Assembly> assemblies = new List<Assembly>();

        public string Root { get; set; }

        public string Suffix { get; set; }

        public void AddAssembly(Assembly assembly)
        {
            assemblies.Add(assembly);
        }

        public Type FindType(string typeName)
        {
            for (var i = 0; i < assemblies.Count; i++)
            {
                var type = assemblies[i].GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
