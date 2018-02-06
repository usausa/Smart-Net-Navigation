﻿namespace Smart.Navigation
{
    using Smart.Mock;

    using Xunit;

    public static class NavigatorComponentSourceTest
    {
        // ------------------------------------------------------------
        // Provider
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorComponentSource()
        {
            var navigator = new NavigatorConfig()
                .UseProvider<MockFormNavigationProvider>()
                .Configure(c => c.Add<IDummyComponent, DummyComponent>())
                .ToNavigator();

            var componentSource = (INavigatorComponentSource)navigator;

            Assert.NotNull(componentSource.Components.Get<IDummyComponent>());
        }

        public interface IDummyComponent
        {
            void Something();
        }

        public class DummyComponent : IDummyComponent
        {
            public void Something()
            {
            }
        }
    }
}