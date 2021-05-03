namespace Smart.Navigation.Mappers
{
    using System;
    using System.Reflection;

    using Smart.Mock;
    using Smart.Navigation.Mappers.Views;
    using Smart.Navigation.Mappers.Views.Children;
    using Smart.Navigation.Mappers.Views.Children.GrandChildren;
    using Smart.Navigation.Mappers.Views.OtherChildren;

    using Xunit;

    public class PathViewMapperTest
    {
        private static Navigator CreateNavigator()
        {
            return new NavigatorConfig()
                .UseMockFormProvider()
                .UsePathViewMapper(option =>
                {
                    option.Root = "Smart.Navigation.Mappers.Views";
                    option.Suffix = "Form";
                    option.AddAssembly(Assembly.GetExecutingAssembly());
                })
                .ToNavigator();
        }

        [Fact]
        public static void UsePathViewMapperAbsolutePath()
        {
            // prepare
            var navigator = CreateNavigator();

            // test
            navigator.Forward("/Parent1");

            Assert.Equal(typeof(Parent1Form), navigator.CurrentView!.GetType());

            navigator.Forward("/Children/Child1");

            Assert.Equal(typeof(Child1Form), navigator.CurrentView.GetType());
        }

        [Fact]
        public static void UsePathViewMapperRelativePath()
        {
            // prepare
            var navigator = CreateNavigator();

            // test
            navigator.Forward("Parent1");
            Assert.Equal(typeof(Parent1Form), navigator.CurrentView!.GetType());

            navigator.Forward("Children/Child1");
            Assert.Equal(typeof(Child1Form), navigator.CurrentView.GetType());

            navigator.Forward("Child2");
            Assert.Equal(typeof(Child2Form), navigator.CurrentView.GetType());

            navigator.Forward("GrandChildren/GrandChild");
            Assert.Equal(typeof(GrandChildForm), navigator.CurrentView.GetType());

            navigator.Forward("../../OtherChildren/OtherChild");
            Assert.Equal(typeof(OtherChildForm), navigator.CurrentView.GetType());

            navigator.Forward("../Parent2");
            Assert.Equal(typeof(Parent2Form), navigator.CurrentView.GetType());
        }

        [Fact]
        public static void Cached()
        {
            // prepare
            var option = new PathViewMapperOptions
            {
                Root = "Smart.Navigation.Mappers.Views",
                Suffix = "Form"
            };
            option.AddAssembly(Assembly.GetExecutingAssembly());
            var mapper = new PathViewMapper(option, new AssignableTypeConstraint(typeof(MockForm)));

            // test
            var descriptor1 = mapper.FindDescriptor("Parent1");
            var descriptor2 = mapper.FindDescriptor("/Parent1");
            Assert.Equal(descriptor1, descriptor2);
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void UsePathViewMapperFailedInvalidIdType()
        {
            // prepare
            var navigator = CreateNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Forward(0));
        }

        [Fact]
        public static void UsePathViewMapperFailedNotExists()
        {
            // prepare
            var navigator = CreateNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Forward("/NotExists"));
        }

        [Fact]
        public static void UsePathViewMapperFailedInvalidType()
        {
            // prepare
            var navigator = CreateNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Forward("/InvalidType"));
        }
    }
}
