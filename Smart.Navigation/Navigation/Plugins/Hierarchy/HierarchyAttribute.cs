namespace Smart.Navigation.Plugins.Hierarchy;

[AttributeUsage(AttributeTargets.Class)]
public sealed class HierarchyAttribute : Attribute
{
    public int Level { get; }

    public HierarchyAttribute(int level)
    {
        Level = level;
    }
}
