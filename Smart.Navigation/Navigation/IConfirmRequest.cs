namespace Smart.Navigation;

public interface IConfirmRequest
{
    bool CanNavigate(INavigationContext context);
}
