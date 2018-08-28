[assembly: Xamarin.Forms.ExportRenderer(typeof(Example.FormsApp.Controls.FunctionButton), typeof(Example.FormsApp.Droid.Renderers.FunctionButtonRenderer))]

namespace Example.FormsApp.Droid.Renderers
{
    using Android.Content;
    using Android.Graphics;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class FunctionButtonRenderer : ButtonRenderer
    {
        public FunctionButtonRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Elevation = 0;
            }

            var fontFamily = e.NewElement.FontFamily?.ToLower();
            if (fontFamily != null && (fontFamily.EndsWith(".otf") || fontFamily.EndsWith(".ttf")))
            {
                Control.Typeface = Typeface.CreateFromAsset(Context.Assets, e.NewElement.FontFamily);
            }
        }
    }
}