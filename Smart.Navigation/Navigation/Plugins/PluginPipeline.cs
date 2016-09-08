namespace Smart.Navigation.Plugins
{
    using System.Collections.Generic;
    using System.Linq;

    public class PluginPipeline : IPluginPipeline
    {
        public IList<IPlugin> Plugins { get; }

        public PluginPipeline(params IPlugin[] plusing)
        {
            Plugins = plusing.ToList();
        }

        public void OnCreate(PluginContext context, object page, object target)
        {
            foreach (var plugin in Plugins)
            {
                plugin.OnCreate(context, page, target);
            }
        }

        public void OnDispose(PluginContext context, object page, object target)
        {
            foreach (var plugin in Plugins)
            {
                plugin.OnDispose(context, page, target);
            }
        }

        public void OnNavigatedFrom(PluginContext context, object page, object target)
        {
            foreach (var plugin in Plugins)
            {
                plugin.OnNavigatedFrom(context, page, target);
            }
        }

        public void OnNavigatedTo(PluginContext context, object page, object target)
        {
            foreach (var plugin in Plugins)
            {
                plugin.OnNavigatedTo(context, page, target);
            }
        }
    }
}
