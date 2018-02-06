//namespace Smart.Navigation.Strategies
//{
//    using System;
//    using System.Threading.Tasks;

//    using Smart.Mock;

//    using Xunit;

//    public class PopStrategyTest
//    {
//        // ------------------------------------------------------------
//        // Navigate
//        // ------------------------------------------------------------

//        [Fact]
//        public static void TestNavigatorPop()
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

//            var form1 = (MockForm)navigator.CurrentView;
//            form1.Focused = "text1";

//            navigator.Push(ViewId.Form2);
//            navigator.Pop();

//            Assert.Equal(1, navigator.StackedCount);
//            var form1B = (MockForm)navigator.CurrentView;
//            Assert.Same(form1, form1B);
//            Assert.Equal(typeof(Form1), form1B.GetType());
//            Assert.True(form1B.IsOpen);
//            Assert.True(form1B.IsVisible);
//            Assert.Equal("text1", form1B.Focused);

//            Assert.Equal(ViewId.Form2, context.Value.FromId);
//            Assert.Equal(ViewId.Form1, context.Value.ToId);
//            Assert.True(context.Value.Attribute.IsRestore());
//        }

//        [Fact]
//        public static void TestNavigatorPopMultiple()
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

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            navigator.Push(ViewId.Form3);
//            navigator.Pop(2);

//            Assert.Equal(1, navigator.StackedCount);
//            var form1 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form1), form1.GetType());
//            Assert.True(form1.IsOpen);
//            Assert.True(form1.IsVisible);

//            Assert.Equal(ViewId.Form3, context.Value.FromId);
//            Assert.Equal(ViewId.Form1, context.Value.ToId);
//            Assert.True(context.Value.Attribute.IsRestore());
//        }

//        [Fact]
//        public static void TestNavigatorPopWithParameter()
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
//            navigator.Push(ViewId.Form2);
//            navigator.Pop(new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        [Fact]
//        public static void TestNavigatorPopMultipleWithParameter()
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

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            navigator.Push(ViewId.Form3);
//            navigator.Pop(2, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        // ------------------------------------------------------------
//        // Async
//        // ------------------------------------------------------------

//        [Fact]
//        public static async Task TestNavigatorPopAsync()
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
//            await navigator.PushAsync(ViewId.Form2);
//            await navigator.PopAsync();

//            Assert.Equal(1, navigator.StackedCount);
//            Assert.Equal(ViewId.Form1, navigator.CurrentViewId);
//        }

//        [Fact]
//        public static async Task TestNavigatorPopAsyncMultiple()
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
//            await navigator.ForwardAsync(ViewId.Form1);
//            await navigator.PushAsync(ViewId.Form2);
//            await navigator.PushAsync(ViewId.Form3);
//            await navigator.PopAsync(2);

//            Assert.Equal(1, navigator.StackedCount);
//            Assert.Equal(ViewId.Form1, navigator.CurrentViewId);
//        }

//        [Fact]
//        public static async Task TestNavigatorPopAsyncWithParameter()
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
//            await navigator.PushAsync(ViewId.Form2);
//            await navigator.PopAsync(new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        [Fact]
//        public static async Task TestNavigatorPopAsyncMultipleWithParameter()
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

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);
//            await navigator.PushAsync(ViewId.Form2);
//            await navigator.PushAsync(ViewId.Form3);
//            await navigator.PopAsync(2, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        // ------------------------------------------------------------
//        // Failed
//        // ------------------------------------------------------------

//        [Fact]
//        public static void TestNavigatorPopFailed()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m =>
//                {
//                    m.Register(1, typeof(Form1));
//                    m.Register(2, typeof(Form2));
//                })
//                .ToNavigator();

//            // test
//            navigator.Forward(1);
//            navigator.Push(2);
//            Assert.Throws<InvalidOperationException>(() => navigator.Pop(2));
//        }

//        [Fact]
//        public static void TestNavigatorPopFailed2()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            // test
//            Assert.Throws<InvalidOperationException>(() => navigator.Pop(0));
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
