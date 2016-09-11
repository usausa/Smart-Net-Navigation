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
        void OnNavigatedFrom(INavigationContext context);

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        void OnNavigatedTo(INavigationContext context);
    }
}
