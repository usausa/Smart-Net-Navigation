namespace Smart.Navigation
{
    using System;
    using System.Reflection;

    using Smart.Mock;

    using Xunit;

    public class NavigatorRegistrationTest
    {
        [Fact]
        public static void TestNavigatorRegistrationByAttribute()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.AutoRegister(Assembly.GetExecutingAssembly());

            // test
            Assert.True(navigator.Forward(Pages.Page1));
            Assert.True(navigator.Forward(Pages.Page2));
        }

        [Fact]
        public static void TestNavigatorRegistrationFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            Assert.Throws<ArgumentNullException>(() => navigator.AutoRegister(null));
        }

        public enum Pages
        {
            Page1,
            Page2
        }

        [Page(Pages.Page1)]
        public class Page1 : MockPage
        {
        }

        [Page(Pages.Page2)]
        public class Page2 : MockPage
        {
        }
    }
}
