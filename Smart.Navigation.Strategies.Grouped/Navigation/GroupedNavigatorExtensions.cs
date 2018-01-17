namespace Smart.Navigation
{
    using System.Threading.Tasks;

    using Smart.Navigation.Strategies;

    public static class GroupedNavigatorExtensions
    {
        // ------------------------------------------------------------
        // Push
        // ------------------------------------------------------------

        public static bool GroupPush(this INavigator navigator, object id)
        {
            return navigator.Navigate(new GroupPushStrategy(id), null);
        }

        public static bool GroupPush(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new GroupPushStrategy(id), parameter);
        }

        // Async

        public static Task<bool> GroupPushAsync(this INavigator navigator, object id)
        {
            return navigator.NavigateAsync(new GroupPushStrategy(id), null);
        }

        public static Task<bool> GroupPushAsync(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new GroupPushStrategy(id), parameter);
        }
    }
}
