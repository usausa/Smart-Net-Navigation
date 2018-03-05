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
        void OnNavigatingFrom(INavigationContext context);

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        void OnNavigatingTo(INavigationContext context);

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        void OnNavigatedTo(INavigationContext context);
    }
}
