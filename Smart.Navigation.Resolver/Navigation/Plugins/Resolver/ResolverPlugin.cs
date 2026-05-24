namespace Smart.Navigation.Plugins.Resolver;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Smart.Navigation.Components;

[RequiresUnreferencedCode("ResolverPlugin uses reflection to scan custom attributes on types. This may not work with trimming.")]
public sealed class ResolverPlugin : PluginBase
{
    private readonly PageContextStorage storage;

    public ResolverPlugin(IServiceProvider serviceProvider)
    {
        storage = (PageContextStorage)serviceProvider.GetService(typeof(PageContextStorage))!;
    }

    public override void OnCreate(IPluginContext pluginContext, object view, object? target)
    {
        if (target is null)
        {
            return;
        }

        foreach (var attribute in target.GetType().GetCustomAttributes<PageContextAttribute>())
        {
            storage.Push(attribute.Name);
        }
    }

    public override void OnClose(IPluginContext pluginContext, object view, object? target)
    {
        if (target is null)
        {
            return;
        }

        foreach (var attribute in target.GetType().GetCustomAttributes<PageContextAttribute>())
        {
            storage.Pop(attribute.Name);
        }
    }
}
