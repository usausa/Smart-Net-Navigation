namespace Smart.Navigation
{
    using System.Threading.Tasks;

    using Smart.Navigation.Strategies;

    public static class GroupedNavigatorExtensions
    {
        // ------------------------------------------------------------
        // Push
        // ------------------------------------------------------------

        public static bool PushOrBringGroup(this INavigator navigator, object id)
        {
            return navigator.Navigate(new PushOrBringGroupStrategy(id), null);
        }

        public static bool PushOrBringGroup(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new PushOrBringGroupStrategy(id), parameter);
        }

        // Async

        public static Task<bool> PushOrBringGroupAsync(this INavigator navigator, object id)
        {
            return navigator.NavigateAsync(new PushOrBringGroupStrategy(id), null);
        }

        public static Task<bool> PushOrBringGroupAsync(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new PushOrBringGroupStrategy(id), parameter);
        }
    }
}
