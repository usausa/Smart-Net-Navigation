namespace Smart.Navigation;

public interface IAsyncNavigationProvider : INavigationProvider
{
    Task OpenViewAsync(object view, INavigationParameter parameter);

    Task CloseViewAsync(object view, INavigationParameter parameter);

    Task ActivateViewAsync(object view, object? state, INavigationParameter parameter);

    Task<object?> DeactivateViewAsync(object view, INavigationParameter parameter);
}
