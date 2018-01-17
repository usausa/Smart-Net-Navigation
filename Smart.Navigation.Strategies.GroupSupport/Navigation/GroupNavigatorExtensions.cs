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

        // ------------------------------------------------------------
        // Pop
        // ------------------------------------------------------------

        public static bool GroupPop(this INavigator navigator)
        {
            return navigator.Navigate(new GroupPopStragety(false), null);
        }

        public static bool GroupPop(this INavigator navigator, INavigationParameter parameter)
        {
            return navigator.Navigate(new GroupPopStragety(false), parameter);
        }

        public static bool GroupPop(this INavigator navigator, bool leaveLast)
        {
            return navigator.Navigate(new GroupPopStragety(leaveLast), null);
        }

        public static bool GroupPop(this INavigator navigator, bool leaveLast, INavigationParameter parameter)
        {
            return navigator.Navigate(new GroupPopStragety(leaveLast), parameter);
        }

        // Async

        public static Task<bool> GroupPopAsync(this INavigator navigator)
        {
            return navigator.NavigateAsync(new GroupPopStragety(false), null);
        }

        public static Task<bool> GroupPopAsync(this INavigator navigator, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new GroupPopStragety(false), parameter);
        }

        public static Task<bool> GroupPopAsync(this INavigator navigator, bool leaveLast)
        {
            return navigator.NavigateAsync(new GroupPopStragety(leaveLast), null);
        }

        public static Task<bool> GroupPopAsync(this INavigator navigator, bool leaveLast, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new GroupPopStragety(leaveLast), parameter);
        }
    }
}
