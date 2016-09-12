namespace Smart.Navigation
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Smart.Navigation.Mocks;

    /// <summary>
    /// ForwardingTest の概要の説明
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
    [TestClass]
    public class ForwardTest
    {
        private Navigator navigator;

        [TestInitialize]
        public void TestInitialize()
        {
            navigator = new Navigator();
            navigator.UseProvider(new MockNavigationProvider());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            navigator.Dispose();
        }

        [TestMethod]
        public void TestSimpleFoward()
        {
            navigator.Register(1, typeof(Page1));
            navigator.Register(2, typeof(Page2));

            // To Page1
            navigator.Forward(1);

            Assert.AreEqual(1, navigator.CurrentPageId);
            Assert.AreEqual(typeof(Page1), navigator.CurrentPage.GetType());

            // To Page2
            navigator.Forward(2);

            Assert.AreEqual(2, navigator.CurrentPageId);
            Assert.AreEqual(typeof(Page2), navigator.CurrentPage.GetType());
        }

        protected class Page1 : MockPageBase
        {
        }

        protected class Page2 : MockPageBase
        {
        }
    }
}
