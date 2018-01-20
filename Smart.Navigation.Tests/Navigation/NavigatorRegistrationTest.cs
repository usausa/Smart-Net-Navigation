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
                .UseMockFormProvider()
                .ToNavigator();

            navigator.AutoRegister(Assembly.GetExecutingAssembly());

            // test
            Assert.True(navigator.Forward(ViewId.Form1));
            Assert.True(navigator.Forward(ViewId.Form2));
        }

        [Fact]
        public static void TestNavigatorRegistrationFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            Assert.Throws<ArgumentNullException>(() => navigator.AutoRegister(null));
        }

        public enum ViewId
        {
            Form1,
            Form2
        }

        [View(ViewId.Form1)]
        public class Form1 : MockForm
        {
        }

        [View(ViewId.Form2)]
        public class Form2 : MockForm
        {
        }
    }
}
