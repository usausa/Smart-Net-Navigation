namespace Smart.Navigation
{
    using System.Diagnostics.CodeAnalysis;

    using Smart.Mock;

    using Xunit;

    public class NavigatorAwareTest
    {
        [Fact]
        public static void NavigatorAware()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(AwareForm));

            var awareForm = (AwareForm)navigator.CurrentView!;
            Assert.Same(navigator, awareForm.Navigator);
        }

        public class AwareForm : MockForm, INavigatorAware
        {
            [AllowNull]
            public INavigator Navigator { get; set; }
        }
    }
}
