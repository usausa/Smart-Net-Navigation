namespace Smart.Navigation;

public interface INavigationProvider
{
    object ResolveTarget(object view);

    void OpenView(object view);

    void CloseView(object view);

    void ActivateView(object view, object? parameter);

    object? DeactivateView(object view);
}
