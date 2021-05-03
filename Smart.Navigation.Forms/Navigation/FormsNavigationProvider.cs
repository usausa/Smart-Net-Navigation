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
            if (container is null)
            {
                throw new InvalidOperationException("Container is unresolved.");
            }

            var v = (View)view;

            AbsoluteLayout.SetLayoutFlags(v, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.HeightProportional);
            AbsoluteLayout.SetLayoutBounds(v, new Rectangle(0, 0, 1, 1));
            container.Children.Add(v);
        }

        public void CloseView(object view)
        {
            var container = resolver.Container;
            if (container is null)
            {
                throw new InvalidOperationException("Container is unresolved.");
            }

            var v = (View)view;

            Cleanup(v);
            (view as IDisposable)?.Dispose();
            (v.BindingContext as IDisposable)?.Dispose();
            v.BindingContext = null;

            container.Children.Remove(v);
        }

        public void ActivateView(object view, object? parameter)
        {
            var v = (View)view;

            v.IsVisible = true;

            if (options.RestoreFocus)
            {
                if (parameter is VisualElement focused)
                {
                    focused.Focus();
                }
                else
                {
                    v.Focus();
                }
            }
        }

        public object? DeactivateView(object view)
        {
            var v = (View)view;

            var parameter = options.RestoreFocus ? GetFocused(v) : null;

            v.IsVisible = false;

            return parameter;
        }

        private static void Cleanup(Element parent)
        {
            while (true)
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
                    parent = contentView.Content;
                    continue;
                }

                break;
            }
        }

        private static VisualElement? GetFocused(Element parent)
        {
            if (parent is VisualElement { IsFocused: true } visualElement)
            {
                return visualElement;
            }

            if (parent is Layout layout)
            {
                foreach (var child in layout.Children)
                {
                    var focused = GetFocused(child);
                    if (focused is not null)
                    {
                        return focused;
                    }
                }
            }

            if (parent is ContentView contentView)
            {
                var focused = GetFocused(contentView.Content);
                if (focused is not null)
                {
                    return focused;
                }
            }

            return null;
        }
    }
}
