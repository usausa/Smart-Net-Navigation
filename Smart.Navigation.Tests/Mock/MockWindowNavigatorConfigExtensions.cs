namespace Smart.Mock
{
    using Smart.Navigation;
    using Smart.Navigation.Mappers;

    public static class MockWindowNavigatorConfigExtensions
    {
        public static NavigatorConfig UseMockWindowProvider(this NavigatorConfig config)
        {
            config.Configure(c =>
            {
                c.RemoveAll<ITypeConstraint>();
                c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(MockWindow)));
            });

            return config.UseProvider<MockWindowNavigationProvider>();
        }
    }
}
