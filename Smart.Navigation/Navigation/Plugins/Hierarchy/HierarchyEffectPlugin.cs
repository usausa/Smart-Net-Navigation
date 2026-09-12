namespace Smart.Navigation.Plugins.Hierarchy;

using System.Reflection;

using Smart.Navigation.Mappers;

public sealed class HierarchyEffectPlugin : PluginBase
{
    private readonly Dictionary<Type, int?> typeLevels = [];

    private readonly IViewMapper viewMapper;

    private readonly HierarchyEffectPluginOptions options;

    public HierarchyEffectPlugin(IViewMapper viewMapper, HierarchyEffectPluginOptions options)
    {
        this.viewMapper = viewMapper;
        this.options = options;
    }

    private int? GetTypeLevel(Type type)
    {
        if (!typeLevels.TryGetValue(type, out var level))
        {
            level = type.GetCustomAttribute<HierarchyAttribute>()?.Level;
            typeLevels[type] = level;
        }

        return level;
    }

    public override void OnPrepareParameter(IPluginContext pluginContext, INavigationContext navigationContext, INavigationParameterPrepare parameter)
    {
        // Explicit effect has priority
        if (parameter.Effect is not null)
        {
            return;
        }

        // Initial navigation
        if (navigationContext.FromId is null)
        {
            return;
        }

        var fromLevel = GetTypeLevel(viewMapper.FindDescriptor(navigationContext.FromId).Type);
        var toLevel = GetTypeLevel(viewMapper.FindDescriptor(navigationContext.ToId).Type);
        if (!fromLevel.HasValue || !toLevel.HasValue)
        {
            return;
        }

        if (toLevel.Value > fromLevel.Value)
        {
            parameter.WithEffect(options.ForwardEffect);
        }
        else if (toLevel.Value < fromLevel.Value)
        {
            parameter.WithEffect(options.BackEffect);
        }
    }
}
