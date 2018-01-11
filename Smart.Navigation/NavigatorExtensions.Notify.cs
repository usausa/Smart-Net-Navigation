namespace Smart.Navigation
{
    /// <summary>
    ///
    /// </summary>
    public static partial class NavigatorExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="parameter"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void Notify(this INavigator navigator, object parameter)
        {
            (navigator.CurrentTarget as INotifySupport)?.NavigatorNotify(parameter);
        }
    }
}
