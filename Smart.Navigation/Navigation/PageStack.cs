namespace Smart.Navigation
{
    public class PageStack
    {
        public PageDescriptor Descriptor { get; }

        public object Page { get; }

        public object RestoreParameter { get; set; }

        public PageStack(PageDescriptor descriptor, object page)
        {
            Descriptor = descriptor;
            Page = page;
        }
    }
}
