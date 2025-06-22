namespace Smart.Navigation;

public interface INavigationEventSupportAsync
{
    Task OnNavigatingFromAsync(INavigationContext context);

    Task OnNavigatingToAsync(INavigationContext context);

    Task OnNavigatedToAsync(INavigationContext context);
}
