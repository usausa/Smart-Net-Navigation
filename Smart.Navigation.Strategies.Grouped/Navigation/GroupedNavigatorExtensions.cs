namespace Smart.Navigation
{
    using System.Threading.Tasks;

    using Smart.Navigation.Strategies;

    public static class GroupedNavigatorExtensions
    {
        // ------------------------------------------------------------
        // Push
        // ------------------------------------------------------------

        public static bool PushAndBringGroup(this INavigator navigator, object id)
        {
            return navigator.Navigate(new PushAndBringGroupStrategy(id), null);
        }

        public static bool PushAndBringGroup(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new PushAndBringGroupStrategy(id), parameter);
        }

        // Async

        public static Task<bool> PushAndBringGroupAsync(this INavigator navigator, object id)
        {
            return navigator.NavigateAsync(new PushAndBringGroupStrategy(id), null);
        }

        public static Task<bool> PushAndBringGroupAsync(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.NavigateAsync(new PushAndBringGroupStrategy(id), parameter);
        }
    }
}
