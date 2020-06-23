using System.Windows.Input;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    public class SettingsPageViewModel : BindableObject
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

        public ICommand LogoutCommand { get; private set; }
        public ICommand ChangePasswordCommand { get; private set; }

        public SettingsPageViewModel()
        {
            LogoutCommand = new Command(execute: async () =>
            {
                var confirm = await Application.Current.MainPage.DisplayAlert(Resx.AppResources.Logout, Resx.AppResources.LogoutConfirm, Resx.AppResources.Yes, Resx.AppResources.Cancel);
                if (confirm)
                {
                    var userService = new Services.UserService();
                    var navigationService = new Services.NavigationService();
                    userService.DoLogout();
                    navigationService.GoToLogin();
                }
            });

            ChangePasswordCommand = new Command(execute: () =>
            {
                var navigationService = new Services.NavigationService();
                navigationService.GoToChangePasswordPage();
            });

            LoadUserData();
        }

        private async void LoadUserData()
        {
            var userService = new Services.UserService();
            UserData = await userService.GetActiveUser();
        }
    }
}
