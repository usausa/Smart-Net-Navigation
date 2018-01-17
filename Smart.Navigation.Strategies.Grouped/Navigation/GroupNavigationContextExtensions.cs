namespace Smart.Navigation
{
    public static class GroupNavigationContextExtensions
    {
        public static bool IsGroup(this INavigationContext context)
        {
            return (context.Attribute & GroupNavigationAttributes.Group) == GroupNavigationAttributes.Group;
        }

        public static bool IsBring(this INavigationContext context)
        {
            return (context.Attribute & GroupNavigationAttributes.Bring) == GroupNavigationAttributes.Bring;
        }
    }
}
