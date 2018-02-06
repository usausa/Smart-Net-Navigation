namespace Smart.Navigation.Mappers
{
    using System;
    using System.Collections.Generic;

    public class PathViewMapper : IViewMapper
    {
        private readonly Dictionary<string, ViewDescriptor> descriptors = new Dictionary<string, ViewDescriptor>();

        private readonly IPathResolver pathResolver;

        private readonly string root;

        private readonly string suffix;

        private string lastPath;

        public PathViewMapper(string root, string suffix, IPathResolver pathResolver)
        {
            this.root = root;
            this.suffix = suffix;
            this.pathResolver = pathResolver ?? new PathResolver();
        }

        public ViewDescriptor FindDescriptor(object id)
        {
            if (id is string str)
            {
                var path = pathResolver.Resolve(lastPath, str);
                if (!descriptors.TryGetValue(path, out var descriptor))
                {
                    var type = PathToType(path);
                    if (type == null)
                    {
                        throw new InvalidOperationException($"View id is invalid id. id=[{id}]");
                    }

                    descriptor = new ViewDescriptor(path, type);
                    descriptors[path] = descriptor;
                }

                return descriptor;
            }

            throw new InvalidOperationException($"View id is invalid id type. id=[{id}]");
        }

        private Type PathToType(string path)
        {
            return Type.GetType($"{root}{path.Replace('/', '.')}{suffix}");
        }

        public void Updated(object id)
        {
            lastPath = (string)id;
        }
    }
}
