//namespace Smart.Navigation.Strategies
//{
//    using System;
//    using System.Threading.Tasks;

//    using Smart.Mock;

//    using Xunit;

//    public class ForwardStrategyTest
//    {
//        // ------------------------------------------------------------
//        // Navigate
//        // ------------------------------------------------------------

//        [Fact]
//        public static void TestNavigatorForward()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m =>
//                {
//                    m.Register(ViewId.Form1, typeof(Form1));
//                    m.Register(ViewId.Form2, typeof(Form2));
//                })
//                .ToNavigator();

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);

//            Assert.Equal(1, navigator.StackedCount);
//            var form1 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form1), form1.GetType());
//            Assert.True(form1.IsOpen);

//            Assert.Null(context.Value.FromId);
//            Assert.Equal(ViewId.Form1, context.Value.ToId);
//            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);

//            navigator.Forward(ViewId.Form2);

//            Assert.Equal(1, navigator.StackedCount);
//            var form2 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form2), form2.GetType());
//            Assert.True(form2.IsOpen);
//            Assert.False(form1.IsOpen);

//            Assert.Equal(ViewId.Form1, context.Value.FromId);
//            Assert.Equal(ViewId.Form2, context.Value.ToId);
//            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
//        }

//        [Fact]
//        public static void TestNavigatorForwardWithStacked()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m =>
//                {
//                    m.Register(ViewId.Form1, typeof(Form1));
//                    m.Register(ViewId.Form2, typeof(Form2));
//                    m.Register(ViewId.Form3, typeof(Form3));
//                })
//                .ToNavigator();

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            navigator.Forward(ViewId.Form3);

//            Assert.Equal(2, navigator.StackedCount);
//            var form3 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form3), form3.GetType());
//            Assert.True(form3.IsOpen);
//        }

//        [Fact]
//        public static void TestNavigatorForwardWithParameter()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m =>
//                {
//                    m.Register(ViewId.Form1, typeof(Form1));
//                    m.Register(ViewId.Form2, typeof(Form2));
//                })
//                .ToNavigator();

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);

//            navigator.Forward(ViewId.Form2, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        // ------------------------------------------------------------
//        // Async
//        // ------------------------------------------------------------

//        [Fact]
//        public static async Task TestNavigatorForwardAsync()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m =>
//                {
//                    m.Register(ViewId.Form1, typeof(Form1));
//                    m.Register(ViewId.Form2, typeof(Form2));
//                })
//                .ToNavigator();

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);

//            Assert.Equal(1, navigator.StackedCount);
//            Assert.Equal(ViewId.Form1, navigator.CurrentViewId);

//            await navigator.ForwardAsync(ViewId.Form2);

//            Assert.Equal(1, navigator.StackedCount);
//            Assert.Equal(ViewId.Form2, navigator.CurrentViewId);
//        }

//        [Fact]
//        public static async Task TestNavigatorForwardAsyncWithParameter()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m =>
//                {
//                    m.Register(ViewId.Form1, typeof(Form1));
//                    m.Register(ViewId.Form2, typeof(Form2));
//                })
//                .ToNavigator();

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);

//            await navigator.ForwardAsync(ViewId.Form2, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        // ------------------------------------------------------------
//        // Failed
//        // ------------------------------------------------------------

//        [Fact]
//        public static void TestNavigatorForwardFailed()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m =>
//                {
//                    m.Register(ViewId.Form1, typeof(Form1));
//                })
//                .ToNavigator();

//            // test
//            navigator.Forward(ViewId.Form1);
//            Assert.Throws<InvalidOperationException>(() => navigator.Push(ViewId.Form2));
//        }

//        // ------------------------------------------------------------
//        // Mock
//        // ------------------------------------------------------------

//        public enum ViewId
//        {
//            Form1,
//            Form2,
//            Form3
//        }

//        public class Form1 : MockForm
//        {
//        }

//        public class Form2 : MockForm
//        {
//        }

//        public class Form3 : MockForm
//        {
//        }
//    }
//}
