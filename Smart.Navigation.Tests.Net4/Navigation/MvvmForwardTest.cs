namespace Smart.Navigation
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Smart.Navigation.Mocks;
    using Smart.Resolver;

    /// <summary>
    /// ForwardingTest の概要の説明
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
    [TestClass]
    public class MvvmForwardTest
    {
        private Navigator navigator;

        private StandardResolver resolver;

        [TestInitialize]
        public void TestInitialize()
        {
            resolver = new StandardResolver();
            navigator = new Navigator();
            navigator
                .UseProvider(new MockMvvmNavigationProvider())
                .UseActivator(type => resolver.Get(type));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            navigator.Dispose();
            resolver.Dispose();
        }

        [TestMethod]
        public void ViewAndViewModelIsSwitchedByForwardAndExit()
        {
            navigator.Register(1, typeof(View1));
            navigator.Register(2, typeof(View2));

            // To Page1
            navigator.Forward(1);

            Assert.AreEqual(1, navigator.CurrentPageId);
            Assert.AreEqual(typeof(View1), navigator.CurrentPage.GetType());
            Assert.AreEqual(typeof(ViewModel1), navigator.CurrentTarget.GetType());

            var view1 = (MockViewBase)navigator.CurrentPage;
            var vm1 = (MockViewModelBase)view1.DataContext;

            Assert.IsTrue(view1.IsOpen);

            // To Page2
            vm1.Navigator.Forward(2);

            Assert.AreEqual(2, navigator.CurrentPageId);
            Assert.AreEqual(typeof(View2), navigator.CurrentPage.GetType());
            Assert.AreEqual(typeof(ViewModel2), navigator.CurrentTarget.GetType());

            var view2 = (MockViewBase)navigator.CurrentPage;
            var vm2 = (MockViewModelBase)view1.DataContext;

            Assert.IsTrue(view2.IsOpen);

            Assert.IsFalse(view1.IsOpen);
            Assert.IsTrue(view1.IsDisposed);
            Assert.IsTrue(vm1.IsDisposed);

            // Exit
            vm2.Navigator.Exit();

            Assert.IsNull(navigator.CurrentPageId);
            Assert.IsNull(navigator.CurrentPage);

            Assert.IsFalse(view2.IsOpen);
            Assert.IsTrue(view2.IsDisposed);
            Assert.IsTrue(vm2.IsDisposed);
        }

        protected class ViewModel1 : MockViewModelBase
        {
        }

        protected class View1 : MockViewBase
        {
            public View1(ViewModel1 vm)
            {
                DataContext = vm;
            }
        }

        protected class ViewModel2 : MockViewModelBase
        {
        }

        protected class View2 : MockViewBase
        {
            public View2(ViewModel2 vm)
            {
                DataContext = vm;
            }
        }
    }
}
