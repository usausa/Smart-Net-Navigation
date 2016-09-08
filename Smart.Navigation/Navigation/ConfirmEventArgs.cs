namespace Smart.Navigation
{
    using System.ComponentModel;

    public class ConfirmEventArgs : CancelEventArgs
    {
        public NavigationContext Context { get; }

        public ConfirmEventArgs(NavigationContext context)
        {
            Context = context;
        }
    }
}
