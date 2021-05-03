namespace Smart.Navigation.Mappers
{
    using System;
    using System.Reflection;

    using Smart.Mock;
    using Smart.Navigation.Attributes;

    using Xunit;

    public class IdViewMapperTest
    {
        [Fact]
        public static void UseIdViewMapper()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .UseIdViewMapper(r => r.AutoRegister(new[] { typeof(Form1), typeof(Form2) }))
                .ToNavigator();

            // test
            navigator.Forward(ViewId.Form1);

            Assert.Equal(typeof(Form1), navigator.CurrentView!.GetType());

            navigator.Forward(ViewId.Form2);

            Assert.Equal(typeof(Form2), navigator.CurrentView!.GetType());
        }

        [Fact]
        public static void UseIdViewMapperFindFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .UseIdViewMapper(_ => { })
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Forward(ViewId.Form1));
        }

        [Fact]
        public static void UseIdViewMapperRegisterFailed()
        {
            Assert.Throws<TargetInvocationException>(() =>
                new NavigatorConfig().UseMockFormProvider().UseIdViewMapper(r => r.Register(1, typeof(string))).ToNavigator());
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
