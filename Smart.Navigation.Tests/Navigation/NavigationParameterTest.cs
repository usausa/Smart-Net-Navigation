namespace Smart.Navigation
{
    using Xunit;

    public static class NavigationParameterTest
    {
        [Fact]
        public static void ParameterGetValueByKey()
        {
            var parameter = new NavigationParameter();

            parameter.SetValue("test", "abc");
            Assert.Equal("abc", parameter.GetValue<string>("test"));
        }

        [Fact]
        public static void ParameterGetValue()
        {
            var parameter = new NavigationParameter();

            parameter.SetValue("abc");
            Assert.Equal("abc", parameter.GetValue<string>());
        }

        [Fact]
        public static void ParameterGetValueOrDefaultByKey()
        {
            var parameter = new NavigationParameter();

            Assert.Null(parameter.GetValueOrDefault<string>("test"));
            parameter.SetValue("test", "abc");
            Assert.Equal("abc", parameter.GetValueOrDefault<string>("test"));
        }

        [Fact]
        public static void ParameterGetValueOrDefault()
        {
            var parameter = new NavigationParameter();

            Assert.Null(parameter.GetValueOrDefault<string>());
            parameter.SetValue("abc");
            Assert.Equal("abc", parameter.GetValueOrDefault<string>());
        }

        [Fact]
        public static void ParameterGetValueOrByKey()
        {
            var parameter = new NavigationParameter();

            Assert.Equal("xyz", parameter.GetValueOr("test", "xyz"));
            parameter.SetValue("test", "abc");
            Assert.Equal("abc", parameter.GetValueOr("test", "xyz"));
        }

        [Fact]
        public static void ParameterGetValueOr()
        {
            var parameter = new NavigationParameter();

            Assert.Equal("xyz", parameter.GetValueOr("xyz"));
            parameter.SetValue("abc");
            Assert.Equal("abc", parameter.GetValueOr("xyz"));
        }
    }
}
