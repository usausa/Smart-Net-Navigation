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
            return page;
        }

        public void OpenPage(object page)
        {
            throw new NotImplementedException();
        }

        public void ClosePage(object page)
        {
            throw new NotImplementedException();
        }

        public void ActivePage(object page, object parameter)
        {
            throw new NotImplementedException();
        }

        public object DectivePage(object page)
        {
            throw new NotImplementedException();
        }
    }
}
