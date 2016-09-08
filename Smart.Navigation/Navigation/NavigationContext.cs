namespace Smart.Navigation
{
    public class NavigationContext
    {
        public object FromPageId { get; }

        public object ToPageId { get; }

        public bool IsStacked { get; }

        public bool IsRestore { get; }

        public INavigationParameter Parameter { get; }

        public NavigationContext(object fromPageId, object toPageId, bool isStacked, bool isRestore, INavigationParameter parameter)
        {
            FromPageId = fromPageId;
            ToPageId = toPageId;
            IsStacked = isStacked;
            IsRestore = isRestore;
            Parameter = parameter;
        }
    }
}
