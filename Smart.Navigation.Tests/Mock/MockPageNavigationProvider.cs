namespace Smart.Mock
{
    using Smart.Navigation;

    public sealed class MockPageNavigationProvider : INavigationProvider
    {
        public object ResolveTarget(object page)
        {
            return page;
        }

        public void OpenPage(NavigationAttributes attributes, object page)
        {
            var view = (MockPage)page;

            view.Show();
        }

        public void ClosePage(NavigationAttributes attributes, object page)
        {
            var view = (MockPage)page;

            view.Dispose();
        }

        public void ActivePage(NavigationAttributes attributes, object page, object parameter)
        {
            var view = (MockPage)page;

            view.IsVisible = true;
            view.Focused = parameter;
        }

        public object DeactivePage(NavigationAttributes attributes, object page)
        {
            var view = (MockPage)page;

            view.IsVisible = false;
            var parameter = view.Focused;
            view.Focused = null;

            return parameter;
        }
    }
}
