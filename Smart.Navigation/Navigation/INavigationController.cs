namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;

    using Smart.Navigation.Mappers;

    public interface INavigationController
    {
        IViewMapper ViewMapper { get; }

        List<ViewStackInfo> ViewStack { get; }

        object CreateView(Type type);

        void OpenView(object view);

        void CloseView(object view);

        void ActivateView(object view, object parameter);

        object DeactivateView(object view);
    }
}
