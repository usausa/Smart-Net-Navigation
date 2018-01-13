namespace Smart.Navigation
{
    using System;

    using System.Collections.Generic;

    public interface INavigationController
    {
        IDictionary<object, PageDescriptor> Descriptors { get; }

        PageStackManager StackManager { get; }

        object CreatePage(Type type);

        void ClosePage(object page);

        void ActivaPage(object page);

        void DeactivePage(object page);
    }
}
