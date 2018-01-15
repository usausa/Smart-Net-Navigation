namespace Smart.Navigation.Strategies
{
    public sealed class StragtegyResult
    {
        public object ToId { get; }

        public NavigationAttributes Attribute { get; }

        public StragtegyResult(object toId, NavigationAttributes attribute)
        {
            ToId = toId;
            Attribute = attribute;
        }
    }
}
