using GamesViewer_Xamarin.Enums;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _viewModel = new LoginPageViewModel();
        private bool BlockLogin { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            _viewModel.EnableLogin = false;
            BindingContext = _viewModel;
        }

        void Button_Register_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRegisterButton();
        }

        void Button_Login_Clicked(object sender, System.EventArgs e)
        {
            if (UpdateRegisterButton())
                DoLogin();
        }

        void Entry_Completed(object sender, System.EventArgs e)
        {
            if (sender == entryUsername)
                entryPassword.Focus();
            else if (UpdateRegisterButton())
                DoLogin();
        }

        private async void DoLogin()
        {
            if (BlockLogin)
                return;

            BlockLogin = true;
            Unfocus();

            var result = await Controllers.UserController.TryLogin(entryUsername.Text.Trim(), entryPassword.Text.Trim());
            if (result.Item1 != UsuarioResultEnum.Ok)
                DependencyService.Get<Interfaces.IToastService>().SendToast(result.Item1.GetText());
            else
            {
                var resultStore = await Controllers.UserController.SaveUserLoginDataAsync(entryUsername.Text.Trim(), entryPassword.Text.Trim());
                if (!resultStore)
                    DependencyService.Get<Interfaces.IToastService>().SendToast(Resx.AppResources.NoAutologinSupport);
                Controllers.UserController.SaveActiveUserId(result.Item2.Id);
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            BlockLogin = false;
        }

        private bool UpdateRegisterButton()
        {
            var canEnable = true;
            if (string.IsNullOrEmpty(entryUsername.Text) || entryUsername.Text.Length < 3)
                canEnable = false;
            if (string.IsNullOrEmpty(entryPassword.Text) || entryPassword.Text.Length < 8)
                canEnable = false;

            _viewModel.EnableLogin = canEnable;
            return canEnable;
        }
    }

    internal class LoginPageViewModel : BindableObject
    {
        private bool _enableLogin;
        public bool EnableLogin
        {
            get
            {
                return _enableLogin;
            }
            set
            {
                _enableLogin = value;
                OnPropertyChanged();
            }
        }
    }
}
