namespace Smart.Navigation.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PathViewMapper : IViewMapper
    {
        private readonly Dictionary<string, ViewDescriptor> descriptors = new Dictionary<string, ViewDescriptor>();

        private readonly PathViewMapperOptions options;

        private readonly ITypeConstraint constraint;

        private string lastPath;

        public PathViewMapper(PathViewMapperOptions options)
            : this(options, null)
        {
        }

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
                    if (type == null)
                    {
                        throw new InvalidOperationException($"View id is invalid id. id=[{id}]");
                    }

                    if (!constraint?.IsValidType(type) ?? false)
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

        private string ResolvePath(string current, string path)
        {
            if (path.StartsWith("/") || String.IsNullOrEmpty(current))
            {
                return NormalizePath(path);
            }

            var index = current.LastIndexOf('/');
            return NormalizePath(current.Substring(0, index + 1) + path);
        }

        private static string NormalizePath(string path)
        {
            var dirs = path.Split('/');

            var length = 0;
            for (var i = 0; i < dirs.Length; i++)
            {
                if ((dirs[i] == ".") || ((i != 0) && (dirs[i].Length == 0)))
                {
                }
                else if (dirs[i] == "..")
                {
                    length--;
                }
                else
                {
                    dirs[length] = dirs[i];
                    length++;
                }
            }

            if ((length <= 0) || ((length == 1) && (dirs[0].Length == 0)))
            {
                return "/";
            }

            var sb = new StringBuilder();
            var start = dirs[0].Length == 0 ? 1 : 0;
            for (var i = start; i < length; i++)
            {
                sb.Append('/');
                sb.Append(dirs[i]);
            }

            return sb.ToString();
        }

        private Type PathToType(string path)
        {
            return Type.GetType($"{options.Root}{path.Replace('/', '.')}{options.Suffix}");
        }

        public void CurrentUpdated(object id)
        {
            lastPath = (string)id;
        }
    }
}
