namespace Smart.Navigation
{
    using System.Threading.Tasks;

    public interface INotifySupportAsync
    {
        Task NavigatorNotifyAsync(object parameter);
    }

    public interface INotifySupportAsync<in T>
    {
        Task NavigatorNotifyAsync(T parameter);
    }
}
