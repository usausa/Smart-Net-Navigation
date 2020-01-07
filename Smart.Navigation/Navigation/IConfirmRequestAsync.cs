namespace Smart.Navigation
{
    using System.Threading.Tasks;

    public interface IConfirmRequestAsync
    {
        ValueTask<bool> CanNavigateAsync(INavigationContext context);
    }
}
