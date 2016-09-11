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

        public void OnCreate(IPluginContext context, object page, object target)
        {
            foreach (var plugin in Plugins)
            {
                plugin.OnCreate(context, page, target);
            }
        }

        public void OnClose(IPluginContext context, object page, object target)
        {
            foreach (var plugin in Plugins)
            {
                plugin.OnClose(context, page, target);
            }
        }

        public void OnNavigatedFrom(IPluginContext context, object page, object target)
        {
            foreach (var plugin in Plugins)
            {
                plugin.OnNavigatedFrom(context, page, target);
            }
        }

        public void OnNavigatedTo(IPluginContext context, object page, object target)
        {
            foreach (var plugin in Plugins)
            {
                plugin.OnNavigatedTo(context, page, target);
            }
        }
    }
}
