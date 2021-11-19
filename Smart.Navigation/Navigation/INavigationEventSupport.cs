namespace Smart.Navigation;

public interface INavigationEventSupport
{
    void OnNavigatingFrom(INavigationContext context);

    void OnNavigatingTo(INavigationContext context);

    void OnNavigatedTo(INavigationContext context);
}
