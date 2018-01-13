namespace Smart.Navigation
{
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public interface IConfirmRequestAsync
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> CanNavigateAsync(INavigationContext context);
    }
}
