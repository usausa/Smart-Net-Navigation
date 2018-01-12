namespace Smart.Navigation
{
    using System;

    public interface INavigationController
    {
        object CreatePage(Type type);

        void ClosePage(object page);

        void ActivaPage(PageStack stack);

        void DeactivePage(PageStack stack);
    }
}
