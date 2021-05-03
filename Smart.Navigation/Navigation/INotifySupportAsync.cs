namespace Smart.Navigation
{
    using System.Threading.Tasks;

    public interface INotifySupportAsync<in T>
    {
        Task NavigatorNotifyAsync(T parameter);
    }
}
