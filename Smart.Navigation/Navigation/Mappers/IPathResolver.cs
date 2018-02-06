namespace Smart.Navigation.Mappers
{
    public interface IPathResolver
    {
        string Resolve(string current, string path);
    }
}