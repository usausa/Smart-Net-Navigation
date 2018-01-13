namespace Smart.Navigation
{
    using Smart.Navigation.Strategies;

    public static partial class NavigatorExtensions
    {
        // Forward

        public static bool Forward(this INavigator navigator, object id)
        {
            return navigator.Navigate(new ForwardStrategy(id), null);
        }

        public static bool Forward(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new ForwardStrategy(id), parameter);
        }

        // Push

        public static bool Push(this INavigator navigator, object id)
        {
            return navigator.Navigate(new PushStrategy(id), null);
        }

        public static bool Push(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new PushStrategy(id), parameter);
        }

        // Pop

        public static bool Pop(this INavigator navigator)
        {
            return navigator.Navigate(new PopStrategy(1), null);
        }

        public static bool Pop(this INavigator navigator, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopStrategy(1), parameter);
        }

        public static bool Pop(this INavigator navigator, int level)
        {
            return navigator.Navigate(new PopStrategy(level), null);
        }

        public static bool Pop(this INavigator navigator, int level, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopStrategy(level), parameter);
        }

        // PopAndForward

        public static bool PopAndForward(this INavigator navigator, object id)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id, 1), null);
        }

        public static bool PopAndForward(this INavigator navigator, object id, int level)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id, level), null);
        }

        public static bool PopAndForward(this INavigator navigator, object id, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id, 1), parameter);
        }

        public static bool PopAndForward(this INavigator navigator, object id, int level, INavigationParameter parameter)
        {
            return navigator.Navigate(new PopAndForwardStrategy(id, level), parameter);
        }
    }
}
