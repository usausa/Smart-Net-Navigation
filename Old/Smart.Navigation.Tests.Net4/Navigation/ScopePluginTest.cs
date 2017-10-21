namespace Smart.Navigation
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Smart.Navigation.Mocks;
    using Smart.Navigation.Plugins.Scope;

    /// <summary>
    /// ForwardingTest の概要の説明
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
    [TestClass]
    public class ScopePluginTest
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
        public void PageContextIsKeepedDuringSameScope()
        {
            navigator.Register(1, typeof(PageHasContext1));
            navigator.Register(2, typeof(PageHasContext2));
            navigator.Register(3, typeof(PageHasNoContext));

            // To PageHasContext1(*)
            navigator.Forward(1);

            var context = ((PageHasContext1)navigator.CurrentPage).Context;

            Assert.IsTrue(context.Active);

            // To PageHasContext2(*)
            navigator.Forward(2);

            var context2 = ((PageHasContext2)navigator.CurrentPage).Context;

            Assert.AreSame(context, context2);
            Assert.IsTrue(context.Active);

            // To PageHasNoContext
            navigator.Forward(3);

            Assert.IsFalse(context.Active);
        }

        [TestMethod]
        public void PageContextIsKeepedDuringStacked()
        {
            navigator.Register(1, typeof(PageHasContext1));
            navigator.Register(2, typeof(PageHasNoContext));

            // To PageHasContext1(*)
            navigator.Forward(1);

            var context = ((PageHasContext1)navigator.CurrentPage).Context;

            Assert.IsTrue(context.Active);

            // To PageHasContext1(*) / PageHasNoContext
            navigator.Push(2);

            Assert.IsTrue(context.Active);

            // To PageHasContext1(*)
            navigator.Pop();

            var context2 = ((PageHasContext1)navigator.CurrentPage).Context;

            Assert.AreSame(context, context2);
            Assert.IsTrue(context.Active);

            // To PageHasNoContext
            navigator.Forward(2);

            Assert.IsFalse(context.Active);
        }

        [TestMethod]
        public void PageContextIsKeepedDuringStackedAndReReference()
        {
            navigator.Register(1, typeof(PageHasContext1));
            navigator.Register(2, typeof(PageHasNoContext));
            navigator.Register(3, typeof(PageHasContext2));

            // To PageHasContext1(*)
            navigator.Forward(1);

            var context = ((PageHasContext1)navigator.CurrentPage).Context;

            Assert.IsTrue(context.Active);

            // To PageHasContext1(*) / PageHasNoContext
            navigator.Push(2);

            Assert.IsTrue(context.Active);

            // To PageHasContext1(*) / PageHasNoContext / PageHasContext2(*)
            navigator.Push(3);

            var context2 = ((PageHasContext2)navigator.CurrentPage).Context;

            Assert.AreSame(context, context2);
            Assert.IsTrue(context.Active);

            // To PageHasContext1(*) / PageHasNoContext
            navigator.Pop();

            Assert.IsTrue(context.Active);

            // To PageHasContext1(*)
            navigator.Pop();

            Assert.IsTrue(context.Active);

            // To PageHasNoContext
            navigator.Forward(2);

            Assert.IsFalse(context.Active);
        }

        [TestMethod]
        public void PageContextIsNotKeepedIfNameNotEqual()
        {
            navigator.Register(1, typeof(PageHasContext1));
            navigator.Register(2, typeof(PageHasContextNameNotEqual));

            // To PageHasContext1(*1)
            navigator.Forward(1);

            var context = ((PageHasContext1)navigator.CurrentPage).Context;

            Assert.IsTrue(context.Active);

            // To PageHasContextNameNotEqual(*2)
            navigator.Forward(2);

            var context2 = ((PageHasContextNameNotEqual)navigator.CurrentPage).Context;

            Assert.AreNotSame(context, context2);
            Assert.IsFalse(context.Active);
            Assert.IsTrue(context2.Active);
        }

        [TestMethod]
        public void PageContextIsCreatedByConcreateType()
        {
            navigator.Register(1, typeof(PageHasContextInterfaceWithType));
            navigator.Register(2, typeof(PageHasNoContext));

            // To PageHasContext1(*1)
            navigator.Forward(1);

            var context = ((PageHasContextInterfaceWithType)navigator.CurrentPage).Context;

            Assert.IsNotNull(context);
            Assert.AreEqual(typeof(ConcreatePageContext), context.GetType());

            // To PageHasNoContext
            navigator.Forward(2);
        }

        protected class PageContext : MockContextBase
        {
        }

        protected interface IAbstractPageContext
        {
        }

        protected class ConcreatePageContext : IAbstractPageContext
        {
        }

        protected class PageHasNoContext : MockPageBase
        {
        }

        protected class PageHasContext1 : MockPageBase
        {
            [Scope]
            public PageContext Context { get; set; }
        }

        protected class PageHasContext2 : MockPageBase
        {
            [Scope]
            public PageContext Context { get; set; }
        }

        protected class PageHasContextNameNotEqual : MockPageBase
        {
            [Scope("Named")]
            public PageContext Context { get; set; }
        }

        protected class PageHasContextInterfaceWithType : MockPageBase
        {
            [Scope(typeof(ConcreatePageContext))]
            public IAbstractPageContext Context { get; set; }
        }
    }
}
