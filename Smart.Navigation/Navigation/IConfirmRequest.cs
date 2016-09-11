namespace Smart.Navigation
{
    using System;

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
