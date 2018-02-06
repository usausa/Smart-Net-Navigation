//namespace Smart.Navigation
//{
//    using Xunit;

//    public static class NavigationParameterTest
//    {
//        [Fact]
//        public static void TestNavigationParameterGetValueByKey()
//        {
//            var parameter = new NavigationParameter();

//            parameter.SetValue("test", "abc");
//            Assert.Equal("abc", parameter.GetValue<string>("test"));
//        }

//        [Fact]
//        public static void TestNavigationParameterGetValue()
//        {
//            var parameter = new NavigationParameter();

//            parameter.SetValue("abc");
//            Assert.Equal("abc", parameter.GetValue<string>());
//        }

//        [Fact]
//        public static void TestNavigationParameterGetValueOrDefaultByKey()
//        {
//            var parameter = new NavigationParameter();

//            Assert.Null(parameter.GetValueOrDefault<string>("test"));
//            parameter.SetValue("test", "abc");
//            Assert.Equal("abc", parameter.GetValueOrDefault<string>("test"));
//        }

//        [Fact]
//        public static void TestNavigationParameterGetValueOrDefault()
//        {
//            var parameter = new NavigationParameter();

//            Assert.Null(parameter.GetValueOrDefault<string>());
//            parameter.SetValue("abc");
//            Assert.Equal("abc", parameter.GetValueOrDefault<string>());
//        }

//        [Fact]
//        public static void TestNavigationParameterGetValueOrByKey()
//        {
//            var parameter = new NavigationParameter();

//            Assert.Equal("xyz", parameter.GetValueOr("test", "xyz"));
//            parameter.SetValue("test", "abc");
//            Assert.Equal("abc", parameter.GetValueOr("test", "xyz"));
//        }

//        [Fact]
//        public static void TestNavigationParameterGetValueOr()
//        {
//            var parameter = new NavigationParameter();

//            Assert.Equal("xyz", parameter.GetValueOr("xyz"));
//            parameter.SetValue("abc");
//            Assert.Equal("abc", parameter.GetValueOr("xyz"));
//        }
//    }
//}
