namespace Smart.Mock
{
    using Smart.Navigation;

    public class MockWindowNavigationProvider : INavigationProvider
    {
        public object ResolveTarget(object view)
        {
            return ((MockWindow)view).Context;
        }

        public void OpenView(object view)
        {
            var window = (MockWindow)view;

            window.IsVisible = true;
        }

        public void CloseView(object view)
        {
            var window = (MockWindow)view;

            window.IsVisible = false;
        }

        public void ActivateView(object view, object parameter)
        {
            var window = (MockWindow)view;

            window.IsVisible = true;
            window.Focused = parameter;
        }

        public object DeactivateView(object view)
        {
            var window = (MockWindow)view;

            window.IsVisible = false;
            var parameter = window.Focused;
            window.Focused = null;

            return parameter;
        }
    }
}
