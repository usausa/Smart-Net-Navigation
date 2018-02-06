namespace Smart.Navigation.Mappers
{
    using System;

    public sealed class PathResolver : IPathResolver
    {
        public string Resolve(string current, string path)
        {
            // TODO /の付加をどうするか？
            if (path.StartsWith("/"))
            {
                return Normalize(path);
            }

            if (String.IsNullOrEmpty(current))
            {
                return "/" + Normalize(path);
            }

            var index = current.LastIndexOf('/');
            return current.Substring(0, index + 1) + Normalize(path);
        }

        public static string Normalize(string path)
        {
            // TODO 通常ケース(絶対かつ上移動なし)？ではなるべくなにもしない

            // TODO stringbuilderをメンバに？、ワーク用、スタックも？

            // TODO .. , last/ ?
            return path;
        }
    }
}
