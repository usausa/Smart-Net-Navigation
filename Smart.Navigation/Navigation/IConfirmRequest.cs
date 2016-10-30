namespace Smart.Navigation
{
    /// <summary>
    ///
    /// </summary>
    public interface IConfirmRequest
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        bool NavigationConfirm(INavigationContext context);
    }
}
