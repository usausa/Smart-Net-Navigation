namespace Smart.Navigation.Plugins.Parameter
{
    using Smart.Mock;

    using Xunit;

    public class ParameterPluginTest
    {
        [Fact]
        public static void TestParameter()
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

        [Fact]
        public static void TestParameterOneWay()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.OneWay, typeof(OneWayPage));

            // test
            navigator.Forward(Pages.Page1);

            var page1 = (Page1)navigator.CurrentPage;
            page1.IntParameter = 123;
            page1.StringParameter = "abc";

            navigator.Push(Pages.OneWay);

            var oneWayPage = (OneWayPage)navigator.CurrentPage;
            Assert.Equal(123, oneWayPage.IntParameter);
            Assert.Null(oneWayPage.StringParameter);

            oneWayPage.IntParameter = 987;
            oneWayPage.StringParameter = "xyz";

            navigator.Pop();
            Assert.Equal(123, page1.IntParameter);
            Assert.Equal("xyz", page1.StringParameter);
        }

        [Fact]
        public static void TestParameterNamed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.NamedPage1, typeof(NamedPage1));
            navigator.Register(Pages.NamedPage2, typeof(NamedPage2));

            // test
            navigator.Forward(Pages.NamedPage1);

            var namedPage1 = (NamedPage1)navigator.CurrentPage;
            namedPage1.Value1 = 123;
            namedPage1.Parameter2 = "abc";

            navigator.Forward(Pages.NamedPage2);

            var namedPage2 = (NamedPage2)navigator.CurrentPage;
            Assert.Equal(123, namedPage2.Parameter1);
            Assert.Equal("abc", namedPage2.Value2);
        }

        [Fact]
        public static void TestParameterConvert()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.ConvertPage1, typeof(ConvertPage1));
            navigator.Register(Pages.ConvertPage2, typeof(ConvertPage2));

            // test
            navigator.Forward(Pages.ConvertPage1);

            var convertPage1 = (ConvertPage1)navigator.CurrentPage;
            convertPage1.Value1 = 123;
            convertPage1.Value2 = "456";

            navigator.Forward(Pages.ConvertPage2);

            var convertPage2 = (ConvertPage2)navigator.CurrentPage;
            Assert.Equal("123", convertPage2.Value1);
            Assert.Equal(456, convertPage2.Value2);
        }

        public enum Pages
        {
            Page1,
            Page2,
            OneWay,
            NamedPage1,
            NamedPage2,
            ConvertPage1,
            ConvertPage2
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

        public class OneWayPage : MockPage
        {
            [Parameter(Directions.Import)]
            public int IntParameter { get; set; }

            [Parameter(Directions.Export)]
            public string StringParameter { get; set; }
        }

        public class NamedPage1 : MockPage
        {
            [Parameter("Parameter1")]
            public int Value1 { get; set; }

            [Parameter]
            public string Parameter2 { get; set; }
        }

        public class NamedPage2 : MockPage
        {
            [Parameter]
            public int Parameter1 { get; set; }

            [Parameter("Parameter2")]
            public string Value2 { get; set; }
        }

        public class ConvertPage1 : MockPage
        {
            [Parameter]
            public int Value1 { get; set; }

            [Parameter]
            public string Value2 { get; set; }
        }

        public class ConvertPage2 : MockPage
        {
            [Parameter]
            public string Value1 { get; set; }

            [Parameter]
            public int Value2 { get; set; }
        }
    }
}
