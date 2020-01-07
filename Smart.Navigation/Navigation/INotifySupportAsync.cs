namespace Smart.Navigation
{
    using System.Threading.Tasks;

    public interface INotifySupportAsync
    {
        ValueTask NavigatorNotifyAsync(object parameter);
    }

    public interface INotifySupportAsync<in T>
    {
        ValueTask NavigatorNotifyAsync(T parameter);
    }
}
