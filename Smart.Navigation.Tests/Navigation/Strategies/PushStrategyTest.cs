namespace Smart.Navigation.Strategies
{
    using System.Threading.Tasks;

    using Smart.Mock;

    using Xunit;

    public class PushStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void Push()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));

            Assert.Equal(1, navigator.StackedCount);
            var form1 = (MockForm)navigator.CurrentView;
            Assert.Equal(typeof(Form1), form1.GetType());
            Assert.True(form1.IsOpen);

            navigator.Push(typeof(Form2));

            Assert.Equal(2, navigator.StackedCount);
            var form2 = (MockForm)navigator.CurrentView;
            Assert.Equal(typeof(Form2), form2.GetType());
            Assert.True(form2.IsOpen);
            Assert.True(form1.IsOpen);
            Assert.False(form1.IsVisible);

            Assert.Equal(typeof(Form1), context.Value.FromId);
            Assert.Equal(typeof(Form2), context.Value.ToId);
            Assert.True(context.Value.Attribute.IsStacked());

            navigator.Push(typeof(Form3));

            Assert.Equal(3, navigator.StackedCount);
            var form3 = (MockForm)navigator.CurrentView;
            Assert.Equal(typeof(Form3), form3.GetType());
            Assert.True(form3.IsOpen);
            Assert.True(form2.IsOpen);
            Assert.False(form2.IsVisible);

            Assert.Equal(typeof(Form2), context.Value.FromId);
            Assert.Equal(typeof(Form3), context.Value.ToId);
            Assert.True(context.Value.Attribute.IsStacked());
        }

        [Fact]
        public static void PushWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));

            navigator.Push(typeof(Form2), new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        [Fact]
        public static async ValueTask TestNavigatorPushAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            await navigator.ForwardAsync(typeof(Form1));

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form1), navigator.CurrentViewId);

            await navigator.PushAsync(typeof(Form2));

            Assert.Equal(2, navigator.StackedCount);
            Assert.Equal(typeof(Form2), navigator.CurrentViewId);

            await navigator.PushAsync(typeof(Form3));

            Assert.Equal(3, navigator.StackedCount);
            Assert.Equal(typeof(Form3), navigator.CurrentViewId);
        }

        [Fact]
        public static async ValueTask TestNavigatorPushAsyncWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));

            await navigator.PushAsync(typeof(Form2), new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

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
