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
        void NavigationConfirm(NavigationContext context, ConfirmOperation operation);
    }
}
