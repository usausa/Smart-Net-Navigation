namespace Smart.Navigation
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class PageAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public object Id { get; }

        /// <summary>
        ///
        /// </summary>
        public object Domain { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        public PageAttribute(object id)
        {
            Id = id;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="domain"></param>
        public PageAttribute(object id, object domain)
        {
            Id = id;
            Domain = domain;
        }
    }
}
