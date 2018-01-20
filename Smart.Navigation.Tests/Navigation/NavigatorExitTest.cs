namespace Smart.Navigation
{
    using Smart.Mock;

    using Xunit;

    public class NavigatorExitTest
    {
        [Fact]
        public static void TestNavigatorExit()
        {
            // prepare
            var called = new Holder<bool>();
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();
            navigator.Exited += (sender, args) => called.Value = true;

            navigator.Register(ViewId.Form1, typeof(Form1));

            // test
            navigator.Forward(ViewId.Form1);

            var form1 = (Form1)navigator.CurrentView;

            navigator.Exit();

            Assert.True(called.Value);
            Assert.False(form1.IsOpen);
        }

        [Fact]
        public static void TestNavigatorExitStacked()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.Form2, typeof(Form2));
            navigator.Register(ViewId.Form3, typeof(Form3));

            // test
            navigator.Forward(ViewId.Form1);

            var form1 = (Form1)navigator.CurrentView;

            navigator.Push(ViewId.Form2);

            var form2 = (Form2)navigator.CurrentView;

            navigator.Push(ViewId.Form3);

            var form3 = (Form3)navigator.CurrentView;

            navigator.Exit();

            Assert.False(form1.IsOpen);
            Assert.False(form2.IsOpen);
            Assert.False(form3.IsOpen);
        }

        public enum ViewId
        {
            Form1,
            Form2,
            Form3
        }

        public class Form1 : MockForm
        {
        }

        public class Form2 : MockForm
        {
        }

        public class Form3 : MockForm
        {
        }
    }
}
