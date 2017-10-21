namespace Smart.Navigation
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Smart.Navigation.Mocks;

    /// <summary>
    /// ForwardingTest の概要の説明
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
    [TestClass]
    public class EventTest
    {
        private Navigator navigator;

        [TestInitialize]
        public void TestInitialize()
        {
            // TODO
            navigator = new Navigator(null);
            //navigator.UseProvider(new MockNavigationProvider());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            navigator.Dispose();
        }

        [TestMethod]
        public void PageNavigateIsCanceledByPage()
        {
            navigator.Register(1, typeof(Page1));
            navigator.Register(2, typeof(Page2));

            // To Page1
            navigator.Forward(1);

            var page1 = (Page1)navigator.CurrentPage;

            // To Page2(Canceled)
            page1.Cancel = true;
            navigator.Forward(2);

            Assert.AreEqual(1, navigator.CurrentPageId);

            // To Page2
            page1.Cancel = false;
            page1.Navigator.Forward(2);

            Assert.AreEqual(2, navigator.CurrentPageId);
        }

        [TestMethod]
        public void PageNavigateIsCanceledByEvent()
        {
            navigator.Register(1, typeof(Page1));
            navigator.Register(2, typeof(Page2));
            navigator.Register(3, typeof(Page2));
            navigator.Confirm += (sender, args) => args.Cancel = (int)args.Context.ToPageId == 2;

            // To Page1
            navigator.Forward(1);

            // To Page2(Canceled)
            navigator.Forward(2);

            Assert.AreEqual(1, navigator.CurrentPageId);

            // To Page2
            navigator.Forward(3);

            Assert.AreEqual(3, navigator.CurrentPageId);
        }

        protected class Page1 : MockPageBase, IConfirmRequest
        {
            public bool Cancel { get; set; }

            public bool NavigationConfirm(INavigationContext context)
            {
                return true;
            }
        }

        protected class Page2 : MockPageBase
        {
        }
    }
}
