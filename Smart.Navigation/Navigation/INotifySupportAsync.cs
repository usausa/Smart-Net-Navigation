namespace Smart.Navigation
{
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public interface INotifySupportAsync
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task NavigatorNotifyAsync(object parameter);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INotifySupportAsync<in T>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task NavigatorNotifyAsync(T parameter);
    }
}
