﻿using System.Threading.Tasks;
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
            var userService = new Services.UserService();
            var autoLoginOk = await userService.TryAutoLogin();
            if (autoLoginOk)
            {
                await Task.Delay(1000);
                Application.Current.MainPage = new NavigationPage(new HomePage());
                return;
            }

            await Task.Delay(1000);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
