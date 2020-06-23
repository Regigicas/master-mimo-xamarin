using System.Windows.Input;
using GamesViewer_Xamarin.Enums;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    class LoginPageViewModel : BindableObject
    {
        private string _username;
        private string _password;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        private bool BlockLogin { get; set; }
        public Command LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public ICommand TextChangeCommand { get; private set; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(execute: DoLogin, canExecute: UpdateRegisterButton);
            RegisterCommand = new Command(execute: () =>
            {
                var navigationService = new Services.NavigationService();
                navigationService.GoToRegister();
            });
        }

        private async void DoLogin()
        {
            if (BlockLogin)
                return;

            BlockLogin = true;
            Application.Current.MainPage.Unfocus();

            var userService = new Services.UserService();
            var toastService = new Services.ToastService();
            var result = await userService.TryLogin(Username.Trim(), Password.Trim());
            if (result.Item1 != UsuarioResultEnum.Ok)
                toastService.SendToast(result.Item1.GetText());
            else
            {
                var resultStore = await userService.SaveUserLoginDataAsync(Username.Trim(), Password.Trim());
                if (!resultStore)
                    toastService.SendToast(Resx.AppResources.NoAutologinSupport);
                userService.SaveActiveUserId(result.Item2.Id);
                var navigationService = new Services.NavigationService();
                navigationService.GoToHome();
            }
            BlockLogin = false;
        }

        private bool UpdateRegisterButton()
        {
            var canEnable = true;
            if (string.IsNullOrEmpty(Username) || Username.Length < 3)
                canEnable = false;
            if (string.IsNullOrEmpty(Password) || Password.Length < 8)
                canEnable = false;

            return canEnable;
        }
    }
}
