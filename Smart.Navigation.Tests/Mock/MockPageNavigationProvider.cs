namespace Smart.Mock
{
    using Smart.Navigation;

    public sealed class MockPageNavigationProvider : INavigationProvider
    {
        public object ResolveTarget(object page)
        {
            return page;
        }

        public void OpenPage(object page)
        {
            var view = (MockPage)page;

            view.Show();
        }

        public void ClosePage(object page)
        {
            var view = (MockPage)page;

            view.Dispose();
        }

        public void ActivePage(object page, object parameter)
        {
            var view = (MockPage)page;

            view.IsVisible = true;
            view.Focused = parameter;
        }

        public object DeactivePage(object page)
        {
            var view = (MockPage)page;

            view.IsVisible = false;
            var parameter = view.Focused;
            view.Focused = null;

            return parameter;
        }
    }
}
