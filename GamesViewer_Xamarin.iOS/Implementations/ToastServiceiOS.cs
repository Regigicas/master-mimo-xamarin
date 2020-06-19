using Foundation;
using GamesViewer_Xamarin.iOS.Implementations;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastServiceiOS))]
namespace GamesViewer_Xamarin.iOS.Implementations
{
    public class ToastServiceiOS : Interfaces.IToastService
    {
        const double LONG_DELAY = 3.5;
        NSTimer alertDelay;
        UIAlertController alert;

        public void SendToast(string message)
        {
            alertDelay = NSTimer.CreateScheduledTimer(LONG_DELAY, (obj) =>
            {
                DismissMessage();
            });

            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void DismissMessage()
        {
            if (alert != null)
                alert.DismissViewController(true, null);

            if (alertDelay != null)
                alertDelay.Dispose();
        }
    }
}
