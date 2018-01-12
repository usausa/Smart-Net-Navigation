namespace Smart.Navigation
{
    public static class NavigationContextExtensions
    {
        public static bool IsStacked(this INavigationContext context)
        {
            return (context.Attribute & NavigationAttribute.Stacked) == NavigationAttribute.Stacked;
        }

        public static bool IsRestore(this INavigationContext context)
        {
            return (context.Attribute & NavigationAttribute.Restore) == NavigationAttribute.Restore;
        }
    }
}
