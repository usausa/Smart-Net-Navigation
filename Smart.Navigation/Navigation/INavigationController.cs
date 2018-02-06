namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;

    using Smart.Navigation.Descriptors;

    public interface INavigationController
    {
        IViewMapper ViewMapper { get; }

        List<ViewStackInfo> ViewStack { get; }

        object CreateView(Type type);

        void OpenView(object view);

        void CloseView(object view);

        void ActiveView(object view, object parameter);

        object DeactiveView(object view);
    }
}
