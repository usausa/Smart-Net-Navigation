namespace Smart.Mock
{
    using Smart.Navigation;
    using Smart.Navigation.Mappers;

    public static class MockFormNavigatorConfigExtensions
    {
        public static NavigatorConfig UseMockFormProvider(this NavigatorConfig config)
        {
            config.Configure(c =>
            {
                c.RemoveAll<ITypeConstraint>();
                c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(MockForm)));
            });

            return config.UseProvider<MockFormNavigationProvider>();
        }
    }
}
