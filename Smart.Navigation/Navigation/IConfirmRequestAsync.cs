namespace Smart.Navigation
{
    using System.Threading.Tasks;

    public interface IConfirmRequestAsync
    {
        Task<bool> CanNavigateAsync(INavigationContext context);
    }
}
