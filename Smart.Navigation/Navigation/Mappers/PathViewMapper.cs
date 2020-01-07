namespace Smart.Navigation.Mappers
{
    using System;
    using System.Collections.Generic;

    public class PathViewMapper : IViewMapper
    {
        private readonly Dictionary<string, ViewDescriptor> descriptors = new Dictionary<string, ViewDescriptor>();

        private readonly PathViewMapperOptions options;

        private readonly ITypeConstraint constraint;

        private string lastPath;

        public PathViewMapper(PathViewMapperOptions options, ITypeConstraint constraint)
        {
            this.options = options;
            this.constraint = constraint;
        }

        public ViewDescriptor FindDescriptor(object id)
        {
            if (id is string path)
            {
                if (descriptors.TryGetValue(path, out var descriptor))
                {
                    return descriptor;
                }

                var normalizePath = ResolvePath(lastPath, path);
                if (!descriptors.TryGetValue(normalizePath, out descriptor))
                {
                    var type = PathToType(normalizePath);
                    if (type is null)
                    {
                        throw new InvalidOperationException($"View id is invalid id. id=[{id}]");
                    }

                    if (!constraint.IsValidType(type))
                    {
                        throw new InvalidOperationException($"View type is invalid type. id=[{id}], type=[{type.FullName}]");
                    }

                    descriptor = new ViewDescriptor(normalizePath, type);
                    descriptors[normalizePath] = descriptor;
                    if (path != normalizePath)
                    {
                        descriptors[path] = descriptor;
                    }
                }

                return descriptor;
            }

            throw new InvalidOperationException($"View id is invalid id type. id=[{id}]");
        }

        private static string ResolvePath(string current, string path)
        {
            if (path.StartsWith(PathHelper.PathSeparatorString, StringComparison.OrdinalIgnoreCase))
            {
                return PathHelper.Normalize(path);
            }

            return PathHelper.Normalize(PathHelper.GetContainer(current) + path);
        }

        private Type PathToType(string path)
        {
            var typeName = $"{options.Root}{path.Replace(PathHelper.PathSeparatorChar, '.')}{options.Suffix}";
            return options.FindType(typeName);
        }

        public void CurrentUpdated(object id)
        {
            lastPath = (string)id;
        }
    }
}
