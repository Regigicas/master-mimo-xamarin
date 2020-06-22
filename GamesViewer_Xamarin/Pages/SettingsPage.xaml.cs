using System.Threading.Tasks;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class SettingsPage : ContentPage
    {
        private SettingsPageViewModel _viewModel = new SettingsPageViewModel();

        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
            LoadUserData();
        }

        private async void LoadUserData()
        {
            var userData = await Controllers.UserController.GetUsuarioActivo();
            _viewModel.UserData = userData;
        }

        internal class SettingsPageViewModel : BindableObject
        {
            private Models.Usuario _userData;
            public Models.Usuario UserData
            {
                get
                {
                    return _userData;
                }
                set
                {
                    _userData = value;
                    OnPropertyChanged();
                }
            }
        }

        async void Button_Logout_Clicked(object sender, System.EventArgs e)
        {
            var confirm = await DisplayAlert(Resx.AppResources.Logout, Resx.AppResources.LogoutConfirm, Resx.AppResources.Yes, Resx.AppResources.Cancel);
            if (confirm)
            {
                await Controllers.UserController.DoLogout();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
