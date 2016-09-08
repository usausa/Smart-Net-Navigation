namespace Smart.Navigation
{
    /// <summary>
    ///
    /// </summary>
    public interface INavigationEventSupport
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        void OnNavigatedFrom(NavigationContext context);

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        void OnNavigatedTo(NavigationContext context);
    }
}
