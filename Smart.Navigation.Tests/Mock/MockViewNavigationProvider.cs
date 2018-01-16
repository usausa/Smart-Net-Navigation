namespace Smart.Mock
{
    using Smart.Navigation;

    public class MockViewNavigationProvider : INavigationProvider
    {
        public object ResolveTarget(object page)
        {
            return ((MockView)page).Context;
        }

        public void OpenPage(object page)
        {
            var view = (MockView)page;

            view.IsVisible = true;
        }

        public void ClosePage(object page)
        {
            var view = (MockView)page;

            view.IsVisible = false;
        }

        public void ActivePage(object page, object parameter)
        {
            var view = (MockView)page;

            view.IsVisible = true;
            view.Focused = parameter;
        }

        public object DeactivePage(object page)
        {
            var view = (MockView)page;

            view.IsVisible = false;
            var parameter = view.Focused;
            view.Focused = null;

            return parameter;
        }
    }
}
