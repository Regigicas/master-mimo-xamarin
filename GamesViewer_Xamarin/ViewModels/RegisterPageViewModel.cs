using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GamesViewer_Xamarin.Enums;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    class RegisterPageViewModel : BindableObject
    {

        private string _username;
        private string _email;
        private string _password;
        private string _passwordRepeat;
        private bool BlockRegister { get; set; }

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
                RegisterCommand.ChangeCanExecute();
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
                RegisterCommand.ChangeCanExecute();
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
                RegisterCommand.ChangeCanExecute();
            }
        }

        public string PasswordRepeat
        {
            get
            {
                return _passwordRepeat;
            }
            set
            {
                _passwordRepeat = value;
                OnPropertyChanged();
                RegisterCommand.ChangeCanExecute();
            }
        }

        public Command RegisterCommand { get; private set; }

        public RegisterPageViewModel()
        {
            BlockRegister = false;
            RegisterCommand = new Command(execute: ClickRegistrar, canExecute: UpdateRegisterButton);
        }

        private async void ClickRegistrar()
        {
            if (BlockRegister)
                return;

            BlockRegister = true;
            Application.Current.MainPage.Unfocus();
            var userService = new Services.UserService();
            var toastService = new Services.ToastService();
            var result = await userService.RegisterUser(Username.Trim(), Email.Trim(), Password.Trim());
            if (result != UsuarioResultEnum.Ok)
            {
                BlockRegister = false;
                toastService.SendToast(result.GetText());
            }
            else
            {
                toastService.SendToast(Resx.AppResources.RegisterOkMsg);
                await Task.Delay(2000);
                var navigationService = new Services.NavigationService();
                navigationService.GoBack();
            }
        }

        private bool UpdateRegisterButton()
        {
            var canEnable = true;
            if (string.IsNullOrEmpty(Username) || Username.Length < 3)
                canEnable = false;
            if (string.IsNullOrEmpty(Email) || !Util.IsEmailValid(Email))
                canEnable = false;
            if (string.IsNullOrEmpty(Password) || Password.Length < 8)
                canEnable = false;
            if (string.IsNullOrEmpty(PasswordRepeat))
                canEnable = false;
            if (Password != PasswordRepeat)
                canEnable = false;

            return canEnable;
        }
    }
}
