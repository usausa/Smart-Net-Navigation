namespace Smart.Navigation;

public interface IConfirmRequestAsync
{
    Task<bool> CanNavigateAsync(INavigationContext context);
}
