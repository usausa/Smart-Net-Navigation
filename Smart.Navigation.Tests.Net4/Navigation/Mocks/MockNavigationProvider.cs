namespace Smart.Navigation.Mocks
{
    using System;

    using Smart.Navigation;

    public class MockNavigationProvider : INavigationProvider
    {
        public bool IsAsync => false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public void BeginInvoke(Action action)
        {
            action();
        }

        public object ResolveTarget(object page)
        {
            return page;
        }

        public void OpenPage(object page)
        {
            (page as MockPageBase)?.Open();
        }

        public void ClosePage(object page)
        {
            (page as MockPageBase)?.Close();
        }

        public void ActivePage(object page, object parameter)
        {
            (page as MockPageBase)?.Active();
        }

        public object DectivePage(object page)
        {
            var mockPage = page as MockPageBase;
            mockPage?.Deactive();
            return mockPage?.Focused;
        }
    }
}
