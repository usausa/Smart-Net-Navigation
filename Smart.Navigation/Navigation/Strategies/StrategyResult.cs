namespace Smart.Navigation.Strategies;

public sealed class StrategyResult
{
    public object ToId { get; }

    public NavigationAttributes Attribute { get; }

    public StrategyResult(object toId, NavigationAttributes attribute)
    {
        ToId = toId;
        Attribute = attribute;
    }
}
