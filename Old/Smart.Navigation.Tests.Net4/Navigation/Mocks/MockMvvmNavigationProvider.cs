namespace Smart.Navigation.Mocks
{
    using System;

    using Smart.Navigation;

    public class MockMvvmNavigationProvider : INavigationProvider
    {
        public bool IsAsync => false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public void BeginInvoke(Action action)
        {
            action();
        }

        public object ResolveTarget(object page)
        {
            return (page as MockViewBase)?.DataContext;
        }

        public void OpenPage(object page)
        {
            (page as MockViewBase)?.Open();
        }

        public void ClosePage(object page)
        {
            (page as MockViewBase)?.Close();
        }

        public void ActivePage(object page, object parameter)
        {
            (page as MockViewBase)?.Active();
        }

        public object DectivePage(object page)
        {
            var mockView = page as MockViewBase;
            mockView?.Deactive();
            return mockView?.Focused;
        }
    }
}
