namespace Smart.Navigation
{
    using System;

    public class NavigationEventArgs : EventArgs
    {
        public INavigationContext Context { get; }

        public NavigationEventArgs(INavigationContext context)
        {
            Context = context;
        }
    }
}
