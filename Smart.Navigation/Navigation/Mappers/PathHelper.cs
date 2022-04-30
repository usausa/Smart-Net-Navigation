namespace Smart.Navigation.Mappers;

using System.Text;

internal static class PathHelper
{
    public const char PathSeparatorChar = '/';

    public const string PathSeparatorString = "/";

    public static string GetContainer(string path)
    {
        if (String.IsNullOrEmpty(path))
        {
            return PathSeparatorString;
        }

        var index = path.LastIndexOf(PathSeparatorChar);
        if (index < 0)
        {
            return PathSeparatorString;
        }

        return path[..(index + 1)];
    }

    public static string Normalize(string path)
    {
        var dirs = path.Split(PathSeparatorChar);

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
            return PathSeparatorString;
        }

        var sb = new StringBuilder();
        var start = dirs[0].Length == 0 ? 1 : 0;
        for (var i = start; i < length; i++)
        {
            sb.Append(PathSeparatorChar);
            sb.Append(dirs[i]);
        }

        return sb.ToString();
    }
}
