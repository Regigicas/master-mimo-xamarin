using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        void Button_Register_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
