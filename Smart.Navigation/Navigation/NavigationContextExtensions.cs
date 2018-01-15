namespace Smart.Navigation
{
    public static class NavigationContextExtensions
    {
        public static bool IsStacked(this INavigationContext context)
        {
            return (context.Attribute & NavigationAttributes.Stacked) == NavigationAttributes.Stacked;
        }

        public static bool IsRestore(this INavigationContext context)
        {
            return (context.Attribute & NavigationAttributes.Restore) == NavigationAttributes.Restore;
        }
    }
}
