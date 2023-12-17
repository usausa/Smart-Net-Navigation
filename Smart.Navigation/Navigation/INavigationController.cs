namespace Smart.Navigation;

using Smart.Navigation.Mappers;

public interface INavigationController
{
    IViewMapper ViewMapper { get; }

#pragma warning disable CA1002
    List<ViewStackInfo> ViewStack { get; }
#pragma warning restore CA1002

    object CreateView(Type type);

    void OpenView(object view);

    void CloseView(object view);

    void ActivateView(object view, object? parameter);

    object? DeactivateView(object view);
}
