namespace Smart.Navigation;

public interface IConfirmRequestAsync
{
    ValueTask<bool> CanNavigateAsync(INavigationContext context);
}
