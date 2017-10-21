namespace Smart.Navigation
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Smart.Navigation.Mocks;
    using Smart.Navigation.Plugins.Parameter;
    using Smart.Resolver;

    /// <summary>
    /// ForwardingTest の概要の説明
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Ignore")]
    [TestClass]
    public class MvvmParameterPluginTest
    {
        private Navigator navigator;

        private StandardResolver resolver;

        [TestInitialize]
        public void TestInitialize()
        {
            // TODO
            resolver = new StandardResolver();
            navigator = new Navigator(null);
            //navigator
            //    .UseProvider(new MockMvvmNavigationProvider())
            //    .UseActivator(type => resolver.Get(type));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            navigator.Dispose();
            resolver.Dispose();
        }

        [TestMethod]
        public void ViewModelParameterIsSetToNextViewModel()
        {
            navigator.Register(1, typeof(ViewFrom));
            navigator.Register(2, typeof(ViewTo));

            // To ViewFrom
            navigator.Forward(1);

            // To ViewTo
            navigator.Forward(2);

            var viewModel = (ViewModelTo)navigator.CurrentTarget;
            Assert.AreEqual(1, viewModel.Parameter);
        }

        protected class ViewModelFrom : MockViewModelBase
        {
            [Parameter]
            public int Parameter { get; set; }

            public override void OnNavigatedFrom(INavigationContext context)
            {
                Parameter = 1;
            }
        }

        protected class ViewFrom : MockViewBase
        {
            public ViewFrom(ViewModelFrom vm)
            {
                DataContext = vm;
            }
        }

        protected class ViewModelTo : MockViewModelBase
        {
            [Parameter]
            public int Parameter { get; set; }
        }

        protected class ViewTo : MockViewBase
        {
            public ViewTo(ViewModelTo vm)
            {
                DataContext = vm;
            }
        }
    }
}
