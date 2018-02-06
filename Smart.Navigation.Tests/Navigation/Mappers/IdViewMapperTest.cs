namespace Smart.Navigation.Mappers
{
    using System;

    using Smart.Mock;
    using Smart.Navigation.Attributes;

    using Xunit;

    public class IdViewMapperTest
    {
        [Fact]
        public static void TestUseIdViewMapper()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .UseIdMapper(r => r.AutoRegister(new[] { typeof(Form1), typeof(Form2) }))
                .ToNavigator();

            // test
            navigator.Forward(ViewId.Form1);

            Assert.Equal(typeof(Form1), navigator.CurrentView.GetType());

            navigator.Forward(ViewId.Form2);

            Assert.Equal(typeof(Form2), navigator.CurrentView.GetType());
        }

        [Fact]
        public static void TestUseIdViewMapperFindFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .UseIdMapper(r => { })
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Forward(ViewId.Form1));
        }

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

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
