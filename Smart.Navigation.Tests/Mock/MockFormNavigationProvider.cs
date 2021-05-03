namespace Smart.Mock
{
    using Smart.Navigation;

    public sealed class MockFormNavigationProvider : INavigationProvider
    {
        public object ResolveTarget(object view)
        {
            return view;
        }

        public void OpenView(object view)
        {
            var form = (MockForm)view;

            form.Show();
        }

        public void CloseView(object view)
        {
            var form = (MockForm)view;

            form.Dispose();
        }

        public void ActivateView(object view, object? parameter)
        {
            var form = (MockForm)view;

            form.IsVisible = true;
            form.Focused = parameter;
        }

        public object? DeactivateView(object view)
        {
            var form = (MockForm)view;

            form.IsVisible = false;
            var parameter = form.Focused;
            form.Focused = null;

            return parameter;
        }
    }
}
