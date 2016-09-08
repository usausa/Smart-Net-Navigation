﻿namespace Smart.Navigation
{
    /// <summary>
    ///
    /// </summary>
    internal class PageStack
    {
        public PageDescripter Descripter { get; }

        public object Page { get; }

        public object RestoreParameter { get; set; }

        public PageStack(PageDescripter descripter, object page)
        {
            Descripter = descripter;
            Page = page;
        }
    }
}
