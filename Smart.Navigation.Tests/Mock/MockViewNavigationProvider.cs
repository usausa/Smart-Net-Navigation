namespace Smart.Mock
{
    using Smart.Navigation;

    public class MockViewNavigationProvider : INavigationProvider
    {
        public object ResolveTarget(object page)
        {
            return ((MockView)page).Context;
        }

        public void OpenPage(NavigationAttributes attributes, object page)
        {
            var view = (MockView)page;

            view.IsVisible = true;
        }

        public void ClosePage(NavigationAttributes attributes, object page)
        {
            var view = (MockView)page;

            view.IsVisible = false;
        }

        public void ActivePage(NavigationAttributes attributes, object page, object parameter)
        {
            var view = (MockView)page;

            view.IsVisible = true;
            view.Focused = parameter;
        }

        public object DeactivePage(NavigationAttributes attributes, object page)
        {
            var view = (MockView)page;

            view.IsVisible = false;
            var parameter = view.Focused;
            view.Focused = null;

            return parameter;
        }
    }
}
