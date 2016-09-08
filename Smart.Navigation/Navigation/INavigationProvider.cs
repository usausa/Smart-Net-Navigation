namespace Smart.Navigation
{
    public interface INavigationProvider
    {
        object ResolveTarget(object page);
    }
}
