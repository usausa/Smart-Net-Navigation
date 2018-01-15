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
            var mock = (MockPage)page;

            mock.Show();
        }

        public void ClosePage(object page)
        {
            var mock = (MockPage)page;

            mock.Dispose();
        }

        public void ActivePage(object page, object parameter)
        {
            var mock = (MockPage)page;

            mock.IsVisible = true;
            mock.Focused = parameter;
        }

        public object DeactivePage(object page)
        {
            var mock = (MockPage)page;

            mock.IsVisible = false;
            var parameter = mock.Focused;
            mock.Focused = null;

            return parameter;
        }
    }
}
