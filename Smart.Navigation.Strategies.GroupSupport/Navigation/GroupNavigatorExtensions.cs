namespace Smart.Navigation
{
    using System.Threading.Tasks;

    using Smart.Navigation.Strategies;

    public static class GroupNavigatorExtensions
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
            return navigator.Navigate(new GroupPopStrategy(false), null);
        }

        public static bool GroupPop(this INavigator navigator, INavigationParameter parameter)
        {
            return navigator.Navigate(new GroupPopStrategy(false), parameter);
        }

        public static bool GroupPop(this INavigator navigator, bool leaveLast)
        {
            return navigator.Navigate(new GroupPopStrategy(leaveLast), null);
        }

        public static bool GroupPop(this INavigator navigator, bool leaveLast, INavigationParameter parameter)
        {
            return navigator.Navigate(new GroupPopStrategy(leaveLast), parameter);
        }

        // Async

        public static Task<bool> GroupPopAsync(this INavigator navigator)
        {
            return navigator.NavigateAsync(new GroupPopStrategy(false), null);
        }

        public static Task<bool> GroupPopAsync(this INavigator navigator, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new GroupPopStrategy(false), parameter);
        }

        public static Task<bool> GroupPopAsync(this INavigator navigator, bool leaveLast)
        {
            return navigator.NavigateAsync(new GroupPopStrategy(leaveLast), null);
        }

        public static Task<bool> GroupPopAsync(this INavigator navigator, bool leaveLast, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new GroupPopStrategy(leaveLast), parameter);
        }
    }
}
