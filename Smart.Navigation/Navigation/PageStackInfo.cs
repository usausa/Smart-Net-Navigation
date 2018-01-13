namespace Smart.Navigation
{
    public class PageStackInfo
    {
        public PageDescriptor Descriptor { get; }

        public object Page { get; }

        public object RestoreParameter { get; set; }

        public PageStackInfo(PageDescriptor descriptor, object page)
        {
            Descriptor = descriptor;
            Page = page;
        }
    }
}
