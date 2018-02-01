namespace Smart.Navigation
{
    using System;

    using Xamarin.Forms;

    public class FormsNavigationProvider : INavigationProvider
    {
        private readonly IContainerResolver resolver;

        private readonly FormsNavigationProviderOptions options;

        public FormsNavigationProvider(IContainerResolver resolver, FormsNavigationProviderOptions options)
        {
            this.resolver = resolver;
            this.options = options;
        }

        public object ResolveTarget(object view)
        {
            return ((View)view).BindingContext;
        }

        public void OpenView(object view)
        {
            var container = resolver.Container;
            if (container == null)
            {
                throw new InvalidOperationException("Container is unresolved.");
            }

            container.Content = (View)view;
        }

        public void CloseView(object view)
        {
            Cleanup((View)view);
            (view as IDisposable)?.Dispose();
            (ResolveTarget(view) as IDisposable)?.Dispose();
        }

        public void ActiveView(object view, object parameter)
        {
            if (options.RestoreFocus)
            {
                if (parameter is VisualElement focused)
                {
                    focused.Focus();
                }
                else
                {
                    ((View)view).Focus();
                }
            }
        }

        public object DeactiveView(object view)
        {
            return options.RestoreFocus ? GetFocused((View)view) : null;
        }

        private static void Cleanup(Element parent)
        {
            if (parent is VisualElement visualElement)
            {
                visualElement.Behaviors.Clear();
                visualElement.Triggers.Clear();
            }

            if (parent is Layout layout)
            {
                foreach (var child in layout.Children)
                {
                    Cleanup(child);
                }
            }

            if (parent is ContentView contentView)
            {
                Cleanup(contentView.Content);
            }
        }

        private static VisualElement GetFocused(Element parent)
        {
            if (parent is VisualElement visualElement && visualElement.IsFocused)
            {
                return visualElement;
            }

            if (parent is Layout layout)
            {
                foreach (var child in layout.Children)
                {
                    var focused = GetFocused(child);
                    if (focused != null)
                    {
                        return focused;
                    }
                }
            }

            if (parent is ContentView contentView)
            {
                var focused = GetFocused(contentView.Content);
                if (focused != null)
                {
                    return focused;
                }
            }

            return null;
        }
    }
}
