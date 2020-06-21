using System.Linq;
using CoreGraphics;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(GamesViewer_Xamarin.iOS.Implementations.LabelShadow), nameof(GamesViewer_Xamarin.iOS.Implementations.LabelShadow))]
namespace GamesViewer_Xamarin.iOS.Implementations
{
    public class LabelShadow : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
                if (effect != null)
                {
                    Control.Layer.ShadowRadius = effect.Radius;
                    Control.Layer.ShadowColor = effect.Color.ToCGColor();
                    Control.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
                    Control.Layer.ShadowOpacity = 1.0f;
                }
            }
            catch {}
        }

        protected override void OnDetached()
        {
        }
    }
}
