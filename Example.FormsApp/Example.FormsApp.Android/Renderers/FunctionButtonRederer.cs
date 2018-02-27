[assembly: Xamarin.Forms.ExportRenderer(typeof(Example.FormsApp.Controls.FunctionButton), typeof(Example.FormsApp.Droid.Renderers.FunctionButtonRederer))]

namespace Example.FormsApp.Droid.Renderers
{
    using Android.Content;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class FunctionButtonRederer : ButtonRenderer
    {
        public FunctionButtonRederer(Context context)
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
        }
    }
}