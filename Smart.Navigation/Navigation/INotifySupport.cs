namespace Smart.Navigation;

public interface INotifySupport<in T>
{
    void NavigatorNotify(T parameter);
}
