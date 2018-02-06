namespace Smart.Navigation
{
    /// <summary>
    ///
    /// </summary>
    public interface INotifySupport
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        void NavigatorNotify(object parameter);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INotifySupport<in T>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        void NavigatorNotify(T parameter);
    }
}
