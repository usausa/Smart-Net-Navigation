namespace Smart.Navigation
{
    /// <summary>
    ///
    /// </summary>
    public class PageStack
    {
        public IPageDescriptor Descriptor { get; }

        public object Page { get; }

        public object RestoreParameter { get; set; }

        public PageStack(IPageDescriptor descriptor, object page)
        {
            Descriptor = descriptor;
            Page = page;
        }
    }
}
