namespace Smart.Navigation
{
    using Smart.Mock;

    using Xunit;

    public class NavigatorExitTest
    {
        [Fact]
        public static void Exit()
        {
            // prepare
            var called = new Holder<bool>();
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();
            navigator.Exited += (_, _) => called.Value = true;

            // test
            navigator.Forward(typeof(Form1));

            var form1 = (Form1)navigator.CurrentView!;

            navigator.Exit();

            Assert.True(called.Value);
            Assert.False(form1.IsOpen);
        }

        [Fact]
        public static void ExitStacked()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));

            var form1 = (Form1)navigator.CurrentView!;

            navigator.Push(typeof(Form2));

            var form2 = (Form2)navigator.CurrentView!;

            navigator.Push(typeof(Form3));

            var form3 = (Form3)navigator.CurrentView!;

            navigator.Exit();

            Assert.False(form1.IsOpen);
            Assert.False(form2.IsOpen);
            Assert.False(form3.IsOpen);
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
