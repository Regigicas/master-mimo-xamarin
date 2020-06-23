using GamesViewer_Xamarin.Enums;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    public class ChangePasswordPageViewModel : BindableObject
    {
        public Command ChangePasswordCommand { get; private set; }
        private string _oldPassword;
        private string _newPassword;
        private string _newPasswordRepeat;

        public string OldPassword
        {
            get
            {
                return _oldPassword;
            }
            set
            {
                _oldPassword = value;
                OnPropertyChanged();
                ChangePasswordCommand.ChangeCanExecute();
            }
        }

        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged();
                ChangePasswordCommand.ChangeCanExecute();
            }
        }

        public string NewPasswordRepeat
        {
            get
            {
                return _newPasswordRepeat;
            }
            set
            {
                _newPasswordRepeat = value;
                OnPropertyChanged();
                ChangePasswordCommand.ChangeCanExecute();
            }
        }

        public ChangePasswordPageViewModel()
        {
            ChangePasswordCommand = new Command(execute: ChangePassword, canExecute: CanUpdateButton);
        }

        public async void ChangePassword()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert(Resx.AppResources.ChangePasswordTitle, Resx.AppResources.ChangePasswordWarning,
                Resx.AppResources.Yes, Resx.AppResources.Cancel);
            if (confirm)
            {
                var userService = new Services.UserService();
                var result = await userService.ChangePassword(OldPassword.Trim(), NewPassword.Trim());
                if (result != UsuarioResultEnum.Ok)
                {
                    var toastService = new Services.ToastService();
                    toastService.SendToast(result.GetText());
                }
                else
                {
                    var navigationService = new Services.NavigationService();
                    userService.DoLogout();
                    navigationService.GoToLogin();
                }
            }
        }

        public bool CanUpdateButton()
        {
            var canEnable = true;
            if (string.IsNullOrEmpty(OldPassword) || OldPassword.Length < 8)
                canEnable = false;
            if (string.IsNullOrEmpty(NewPassword) || NewPassword.Length < 8)
                canEnable = false;
            if (string.IsNullOrEmpty(NewPasswordRepeat) || NewPasswordRepeat.Length < 8)
                canEnable = false;
            if (NewPassword != NewPasswordRepeat)
                canEnable = false;

            return canEnable;
        }
    }
}
