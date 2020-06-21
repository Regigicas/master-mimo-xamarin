using System.Linq;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Xamarin")]
[assembly:ExportEffect (typeof(GamesViewer_Xamarin.Droid.Implementations.LabelShadow), nameof(GamesViewer_Xamarin.Droid.Implementations.LabelShadow))]
namespace GamesViewer_Xamarin.Droid.Implementations
{
    public class LabelShadow : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control as Android.Widget.TextView;
                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
                if (effect != null)
                {
                    float radius = effect.Radius;
                    float distanceX = effect.DistanceX;
                    float distanceY = effect.DistanceY;
                    Android.Graphics.Color color = effect.Color.ToAndroid();
                    control.SetShadowLayer(radius, distanceX, distanceY, color);
                }
            }
            catch {}
        }

        protected override void OnDetached()
        {
        }
    }
}
