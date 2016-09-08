namespace Smart.Navigation.Plugins
{
    using System.Collections.Generic;

    public interface IPluginPipeline
    {
        IList<IPlugin> Plugins { get; }

        void OnCreate(PluginContext context, object page, object target);

        void OnDispose(PluginContext context, object page, object target);

        void OnNavigatedFrom(PluginContext context, object page, object target);

        void OnNavigatedTo(PluginContext context, object page, object target);
    }
}
