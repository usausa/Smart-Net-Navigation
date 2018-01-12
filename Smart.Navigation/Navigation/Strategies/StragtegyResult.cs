namespace Smart.Navigation.Strategies
{
    public class StragtegyResult
    {
        public object ToId { get; }

        public NavigationAttribute Attribute { get; }

        public StragtegyResult(object toId, NavigationAttribute attribute)
        {
            ToId = toId;
            Attribute = attribute;
        }
    }
}
