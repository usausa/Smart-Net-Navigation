namespace Smart.Navigation
{
    //    using System;

    using Smart.Navigation.Strategies;

    public static partial class NavigatorExtensions
    {
        public static void Forward(this INavigator navigator, object id)
        {
            navigator.Navigate(new ForwardStrategy(id), null);
        }

        public static void Forward(this INavigator navigator, object id, INavigationParameter parameter)
        {
            navigator.Navigate(new ForwardStrategy(id), parameter);
        }

//        public static void Push(this INavigator navigator, object id)
//        {
//            throw new NotImplementedException();
//        }

//        public static void Push(this INavigator navigator, object id, INavigationParameter parameter)
//        {
//            throw new NotImplementedException();
//        }

//        public static void Pop(this INavigator navigator)
//        {
//            // TODO level nullable?
//            throw new NotImplementedException();
//        }

//        public static void Pop(this INavigator navigator, INavigationParameter parameter)
//        {
//            throw new NotImplementedException();
//        }

//        public static void Pop(this INavigator navigator, int level)
//        {
//            throw new NotImplementedException();
//        }

//        public static void Pop(this INavigator navigator, int level, INavigationParameter parameter)
//        {
//            throw new NotImplementedException();
//        }

//        public static void PopAndForward(this INavigator navigator, object id)
//        {
//            throw new NotImplementedException();
//        }

//        public static void PopAndForward(this INavigator navigator, object id, int level)
//        {
//            throw new NotImplementedException();
//        }

//        public static void PopAndForward(this INavigator navigator, object id, INavigationParameter parameter)
//        {
//            throw new NotImplementedException();
//        }

//        public static void PopAndForward(this INavigator navigator, object id, int level, INavigationParameter parameter)
//        {
//            throw new NotImplementedException();
//        }
    }
}
