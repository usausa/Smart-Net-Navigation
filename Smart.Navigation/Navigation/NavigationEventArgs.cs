namespace Smart.Navigation
{
    using System;

    public sealed class NavigationEventArgs : EventArgs
    {
        public INavigationContext Context { get; }

        public object FromPage { get; }

        public object FromTarget { get; }

        public object ToPage { get; }

        public object ToTarget { get; }

        public NavigationEventArgs(
            INavigationContext context,
            object fromPage,
            object fromTarget,
            object toPage,
            object toTarget)
        {
            Context = context;
            FromPage = fromPage;
            FromTarget = fromTarget;
            ToPage = toPage;
            ToTarget = toTarget;
        }
    }
}
