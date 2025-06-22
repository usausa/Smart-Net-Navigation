namespace Smart.Navigation;

public interface INavigationEventSupportAsync
{
    ValueTask OnNavigatingFromAsync(INavigationContext context);

    ValueTask OnNavigatingToAsync(INavigationContext context);

    ValueTask OnNavigatedToAsync(INavigationContext context);
}
