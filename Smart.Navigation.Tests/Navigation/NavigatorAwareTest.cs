namespace Smart.Navigation
{
    using Smart.Mock;

    using Xunit;

    public class NavigatorAwareTest
    {
        [Fact]
        public static void TestNavigatorAware()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.AwareForm, typeof(AwareForm));

            // test
            navigator.Forward(ViewId.AwareForm);

            var awareForm = (AwareForm)navigator.CurrentView;
            Assert.Same(navigator, awareForm.Navigator);
        }

        public enum ViewId
        {
            AwareForm
        }

        public class AwareForm : MockForm, INavigatorAware
        {
            public INavigator Navigator { get; set; }
        }
    }
}
