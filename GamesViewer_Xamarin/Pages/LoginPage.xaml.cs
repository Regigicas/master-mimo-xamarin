using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Rotate();
        }

        async void Rotate()
        {
            await logoApp.RotateTo(360.0f, 1000, Easing.Linear);
        }
    }
}
