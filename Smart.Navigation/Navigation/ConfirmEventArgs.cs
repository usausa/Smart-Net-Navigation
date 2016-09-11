namespace Smart.Navigation
{
    using System.ComponentModel;

    public class ConfirmEventArgs : CancelEventArgs
    {
        public INavigationContext Context { get; }

        public ConfirmEventArgs(INavigationContext context)
        {
            Context = context;
        }
    }
}
