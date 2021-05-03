namespace Smart.Navigation.Strategies
{
    using System.Threading.Tasks;

    using Smart.Mock;

    using Xunit;

    public class ForwardStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void Forward()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));

            Assert.Equal(1, navigator.StackedCount);
            var form1 = (MockForm)navigator.CurrentView!;
            Assert.Equal(typeof(Form1), form1.GetType());
            Assert.True(form1.IsOpen);

            Assert.Null(context.Value.FromId);
            Assert.Equal(typeof(Form1), context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);

            navigator.Forward(typeof(Form2));

            Assert.Equal(1, navigator.StackedCount);
            var form2 = (MockForm)navigator.CurrentView!;
            Assert.Equal(typeof(Form2), form2.GetType());
            Assert.True(form2.IsOpen);
            Assert.False(form1.IsOpen);

            Assert.Equal(typeof(Form1), context.Value.FromId);
            Assert.Equal(typeof(Form2), context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
        }

        [Fact]
        public static void ForwardWithStacked()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.Forward(typeof(Form3));

            Assert.Equal(2, navigator.StackedCount);
            var form3 = (MockForm)navigator.CurrentView!;
            Assert.Equal(typeof(Form3), form3.GetType());
            Assert.True(form3.IsOpen);
        }

        [Fact]
        public static void ForwardWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));

            navigator.Forward(typeof(Form2), new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        [Fact]
        public static async ValueTask TestNavigatorForwardAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            await navigator.ForwardAsync(typeof(Form1));

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form1), navigator.CurrentViewId);

            await navigator.ForwardAsync(typeof(Form2));

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form2), navigator.CurrentViewId);
        }

        [Fact]
        public static async ValueTask TestNavigatorForwardAsyncWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));

            await navigator.ForwardAsync(typeof(Form2), new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

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
