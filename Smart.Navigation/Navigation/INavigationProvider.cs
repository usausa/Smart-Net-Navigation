namespace Smart.Navigation
{
    using System;

    public interface INavigationProvider
    {
        object ResolveTarget(object page);

        void OpenPage(object page);

        void ClosePage(object page);

        void ActivePage(object page, object parameter);

        object DectivePage(object page);
    }
}
