using Android.Widget;
using GamesViewer_Xamarin.Droid.Implementations;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastServiceAndroid))]
namespace GamesViewer_Xamarin.Droid.Implementations
{
    public class ToastServiceAndroid : Interfaces.IToastService
    {
        public void SendToast(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}
