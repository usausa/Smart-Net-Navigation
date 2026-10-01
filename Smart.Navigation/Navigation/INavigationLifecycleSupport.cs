namespace Smart.Navigation;

public interface INavigationLifecycleSupport
{
    void OnActivated();

    void OnDeactivated();
}
