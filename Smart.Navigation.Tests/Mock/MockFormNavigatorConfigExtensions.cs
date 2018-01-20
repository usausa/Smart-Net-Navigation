namespace Smart.Mock
{
    using Smart.Navigation;

    public static class MockFormNavigatorConfigExtensions
    {
        public static NavigatorConfig UseMockFormProvider(this NavigatorConfig config)
        {
            return config.UseProvider<MockFormNavigationProvider>();
        }
    }
}
