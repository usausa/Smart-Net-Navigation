namespace Smart.Navigation
{
    using System.Collections.Generic;

    public class PageStackManager
    {
        public List<PageStack> Stacked { get; } = new List<PageStack>();

        public PageStack CurrentStack => Stacked.Count > 0 ? Stacked[Stacked.Count - 1] : null;

        public object CurrentPageId => CurrentStack?.Descriptor.Id;

        public object CurrentPageDomain => CurrentStack?.Descriptor.Domain;

        public object CurrentPage => CurrentStack?.Page;
    }
}
