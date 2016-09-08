namespace Smart.Navigation
{
    using System;

    public class NavigationEventArgs : EventArgs
    {
        public NavigationContext Context { get; }

        public NavigationEventArgs(NavigationContext context)
        {
            Context = context;
        }
    }
}
