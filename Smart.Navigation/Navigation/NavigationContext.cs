namespace Smart.Navigation
{
    public sealed class NavigationContext : INavigationContext
    {
        public object FromId { get; }

        public object ToId { get; }

        public NavigationAttribute Attribute { get; }

        public INavigationParameter Parameter { get; }

        public NavigationContext(object fromId, object toId, NavigationAttribute attribute, INavigationParameter parameter)
        {
            FromId = fromId;
            ToId = toId;
            Attribute = attribute;
            Parameter = parameter;
        }
    }
}
