namespace Smart.Navigation
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Smart.Navigation.Mocks;
    using Smart.Navigation.Plugins.Parameter;

    /// <summary>
    /// ForwardingTest の概要の説明
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
    [TestClass]
    public class ParameterPluginTest
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
        public void PageParameterIsSetToNextPage()
        {
            navigator.Register(1, typeof(PageFrom));
            navigator.Register(2, typeof(PageTo));

            // To PageFrom
            navigator.Forward(1);

            // To PageTo
            navigator.Forward(2);

            var page = (PageTo)navigator.CurrentPage;
            Assert.AreEqual(1, page.Parameter);
        }

        [TestMethod]
        public void PageParameterIsConvertAndSetToNextPage()
        {
            navigator.Register(1, typeof(PageFrom));
            navigator.Register(2, typeof(PageToConvert));

            // To PageFrom
            navigator.Forward(1);

            // To PageToConvert
            navigator.Forward(2);

            var page = (PageToConvert)navigator.CurrentPage;
            Assert.AreEqual("1", page.Parameter);
        }

        [TestMethod]
        public void PageParameterIsSetToNextPageWithNamedParameter()
        {
            navigator.Register(1, typeof(PageFrom));
            navigator.Register(2, typeof(PageToNamed));

            // To PageFrom
            navigator.Forward(1);

            // To PageToNamed
            navigator.Forward(2);

            var page = (PageToNamed)navigator.CurrentPage;
            Assert.AreEqual(1, page.Argument);
        }

        [TestMethod]
        public void PageParameterIsNotSetToNextPageIfToPageParameterIsExportOnly()
        {
            navigator.Register(1, typeof(PageFrom));
            navigator.Register(2, typeof(PageExport));

            // To PageFrom
            navigator.Forward(1);

            // To PageExport
            navigator.Forward(2);

            var page = (PageExport)navigator.CurrentPage;
            Assert.AreNotEqual(1, page.Parameter);
        }

        [TestMethod]
        public void PageParameterIsNotSetToNextPageIfFromPageParameterIsImportOnly()
        {
            navigator.Register(1, typeof(PageImport));
            navigator.Register(2, typeof(PageTo));

            // To PageImport
            navigator.Forward(1);

            // To PageTo
            navigator.Forward(2);

            var page = (PageTo)navigator.CurrentPage;
            Assert.AreNotEqual(1, page.Parameter);
        }

        protected class PageFrom : MockPageBase
        {
            [Parameter]
            public int Parameter { get; set; }

            public override void OnNavigatedFrom(INavigationContext context)
            {
                Parameter = 1;
            }
        }

        protected class PageTo : MockPageBase
        {
            [Parameter]
            public int Parameter { get; set; }
        }

        protected class PageToConvert : MockPageBase
        {
            [Parameter]
            public string Parameter { get; set; }
        }

        protected class PageToNamed : MockPageBase
        {
            [Parameter("Parameter")]
            public int Argument { get; set; }
        }

        protected class PageExport : MockPageBase
        {
            [Parameter(Direction.Export)]
            public int Parameter { get; set; }
        }

        protected class PageImport : MockPageBase
        {
            [Parameter(Direction.Import)]
            public int Parameter { get; set; }

            public override void OnNavigatedFrom(INavigationContext context)
            {
                Parameter = 1;
            }
        }
    }
}
