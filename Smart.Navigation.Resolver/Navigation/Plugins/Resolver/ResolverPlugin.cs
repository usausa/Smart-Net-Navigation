namespace Smart.Navigation.Plugins.Resolver
{
    using System.Reflection;

    using Smart.Navigation.Components;

    public sealed class ResolverPlugin : PluginBase
    {
        private readonly PageContextStorage storage;

        public ResolverPlugin(IActivator activator)
        {
            storage = (PageContextStorage)activator.Resolve(typeof(PageContextStorage));
        }

        public override void OnCreate(IPluginContext context, object view, object target)
        {
            foreach (var attribute in target.GetType().GetCustomAttributes<PageContextAttribute>())
            {
                storage.Push(attribute.Name);
            }
        }

        public override void OnClose(IPluginContext context, object view, object target)
        {
            foreach (var attribute in target.GetType().GetCustomAttributes<PageContextAttribute>())
            {
                storage.Pop(attribute.Name);
            }
        }
    }
}
