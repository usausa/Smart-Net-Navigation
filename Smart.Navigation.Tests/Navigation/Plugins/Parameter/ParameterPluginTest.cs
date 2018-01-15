namespace Smart.Navigation.Plugins.Parameter
{
    using Smart.Mock;

    using Xunit;

    public class ParameterPluginTest
    {
        [Fact]
        public static void TestParameterWhenForward()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            // test
            navigator.Forward(Pages.Page1);

            var page1 = (Page1)navigator.CurrentPage;
            page1.IntParameter = 123;
            page1.StringParameter = "abc";

            navigator.Forward(Pages.Page2);

            Assert.Equal(1, navigator.StackedCount);
            var page2 = (Page2)navigator.CurrentPage;
            Assert.Equal(123, page2.IntParameter);
            Assert.Equal("abc", page2.StringParameter);
        }

        public enum Pages
        {
            Page1,
            Page2
        }

        public class Page1 : MockPage
        {
            [Parameter]
            public int IntParameter { get; set; }

            [Parameter]
            public string StringParameter { get; set; }
        }

        public class Page2 : MockPage
        {
            [Parameter]
            public int IntParameter { get; set; }

            [Parameter]
            public string StringParameter { get; set; }
        }
    }
}
