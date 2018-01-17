namespace Smart.Navigation
{
    public interface INavigationProvider
    {
        object ResolveTarget(object page);

        void OpenPage(NavigationAttributes attributes, object page);

        void ClosePage(NavigationAttributes attributes, object page);

        void ActivePage(NavigationAttributes attributes, object page, object parameter);

        object DeactivePage(NavigationAttributes attributes, object page);
    }
}
