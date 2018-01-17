namespace Smart.Navigation
{
    public static class GroupedNavigationAttributesExtensions
    {
        public static bool IsGroup(this NavigationAttributes attributes)
        {
            return (attributes & GroupedNavigationAttributes.Group) == GroupedNavigationAttributes.Group;
        }

        public static bool IsBring(this NavigationAttributes attributes)
        {
            return (attributes & GroupedNavigationAttributes.Bring) == GroupedNavigationAttributes.Bring;
        }
    }
}
