using Xamarin.Forms;

namespace GamesViewer_Xamarin.Services
{
    public class ToastService
    {
        public void SendToast(string text)
        {
            DependencyService.Get<Interfaces.IToastService>().SendToast(text);
        }
    }
}
