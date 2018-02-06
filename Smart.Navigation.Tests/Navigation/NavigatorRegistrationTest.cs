//namespace Smart.Navigation
//{
//    using System;
//    using System.Reflection;

//    using Smart.Mock;
//    using Smart.Navigation.Attributes;

//    using Xunit;

//    public class NavigatorRegistrationTest
//    {
//        [Fact]
//        public static void TestNavigatorRegistrationByAttribute()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m => m.AutoRegister(Assembly.GetExecutingAssembly()))
//                .ToNavigator();

//            // test
//            Assert.True(navigator.Forward(ViewId.Form1));
//            Assert.True(navigator.Forward(ViewId.Form2));
//        }

//        [Fact]
//        public static void TestNavigatorRegistrationFailed()
//        {
//            // test
//            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m => m.AutoRegister(null)));
//        }

//        public enum ViewId
//        {
//            Form1,
//            Form2
//        }

//        [View(ViewId.Form1)]
//        public class Form1 : MockForm
//        {
//        }

//        [View(ViewId.Form2)]
//        public class Form2 : MockForm
//        {
//        }
//    }
//}
