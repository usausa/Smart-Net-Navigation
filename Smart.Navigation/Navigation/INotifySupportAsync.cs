namespace Smart.Navigation;

public interface INotifySupportAsync<in T>
{
    Task NavigatorNotifyAsync(T parameter);
}
