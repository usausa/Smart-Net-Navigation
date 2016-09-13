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

            var page1 = (MockPageBase)navigator.CurrentPage;

            Assert.IsTrue(page1.IsOpen);

            // To Page2
            page1.Navigator.Forward(2);

            Assert.AreEqual(2, navigator.CurrentPageId);
            Assert.AreEqual(typeof(Page2), navigator.CurrentPage.GetType());

            var page2 = (MockPageBase)navigator.CurrentPage;

            Assert.IsTrue(page2.IsOpen);

            Assert.IsFalse(page1.IsOpen);
            Assert.IsTrue(page1.IsDisposed);

            // Exit
            page2.Navigator.Exit();

            Assert.IsNull(navigator.CurrentPageId);
            Assert.IsNull(navigator.CurrentPage);

            Assert.IsFalse(page2.IsOpen);
            Assert.IsTrue(page2.IsDisposed);
        }

        protected class Page1 : MockPageBase
        {
        }

        protected class Page2 : MockPageBase
        {
        }
    }
}
