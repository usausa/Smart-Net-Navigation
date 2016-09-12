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
        /// <param name="operation"></param>
        void NavigationConfirm(INavigationContext context, ConfirmOperation operation);
    }
}
