using System;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Controllers;
using GamesViewer_Xamarin.Interfaces;
using GamesViewer_Xamarin.Resx;
using Refit;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            TryAutoLogin();
        }

        private async void TryAutoLogin()
        {
            var autoLoginOk = await UserController.tryAutoLogin();
            if (autoLoginOk)
            {
                // go home
                return;
            }

            await Task.Delay(1000);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
