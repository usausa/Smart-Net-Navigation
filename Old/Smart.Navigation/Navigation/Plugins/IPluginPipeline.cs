namespace Smart.Navigation.Plugins
{
    using System.Collections.Generic;

    public interface IPluginPipeline
    {
        IList<IPlugin> Plugins { get; }

        void OnPreProcess(IPluginContext context);

        void OnNavigatedFrom(IPluginContext context, object page, object target);

        void OnClose(IPluginContext context, object page, object target);

        void OnCreate(IPluginContext context, object page, object target);

        void OnNavigatedTo(IPluginContext context, object page, object target);

        void OnPostProcess(IPluginContext context);
    }
}
