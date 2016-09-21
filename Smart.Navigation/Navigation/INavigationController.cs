namespace Smart.Navigation
{
    using System;

    using Smart.ComponentModel;
    using Smart.Navigation.Plugins;

    public interface INavigationController
    {
        // TODOメソッド
        // TODO 後で不要なものを非公開に
        IComponentContainer Components { get; }

        INavigationProvider Provider { get; }

        IPluginPipeline Pipeline { get; }

        IPluginContext PluginContext { get; }

        INavigationContext NavigationContext { get; }

        PageStackManager StackManager { get; }

        object CreatePage(Type type);

        void ClosePage(object page);

        void ActivaPage();

        void DeactivePage();

        void ProcessNavigatedFrom();

        void ProcessNavigatedTo();
    }
}