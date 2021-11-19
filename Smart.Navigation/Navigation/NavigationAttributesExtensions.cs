namespace Smart.Navigation;

public static class NavigationAttributesExtensions
{
    public static bool IsStacked(this NavigationAttributes attributes)
    {
        return (attributes & NavigationAttributes.Stacked) == NavigationAttributes.Stacked;
    }

    public static bool IsRestore(this NavigationAttributes attributes)
    {
        return (attributes & NavigationAttributes.Restore) == NavigationAttributes.Restore;
    }
}
