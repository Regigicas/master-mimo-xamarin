using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Enums;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class RegisterPage : ContentPage, INotifyPropertyChanged
    {
        private Dictionary<VisualElement, VisualElement> _completedNext;
        private RegisterPageViewModel _viewModel = new RegisterPageViewModel();
        private bool BlockRegister { get; set; }

        public RegisterPage()
        {
            InitializeComponent();
            _viewModel.EnableRegister = false;
            BindingContext = _viewModel;
            _completedNext = new Dictionary<VisualElement, VisualElement>
            {
                { entryUsername,       entryEmail },
                { entryEmail,          entryPassword },
                { entryPassword,       entryPasswordRetype },
                { entryPasswordRetype, null }
            };
        }

        void Button_Register_Clicked(object sender, System.EventArgs e)
        {
            if (UpdateRegisterButton())
                ClickRegistrar();
        }

        void Entry_Click_Next(object sender, System.EventArgs e)
        {
            _completedNext.TryGetValue(sender as VisualElement, out VisualElement nextFocus);
            if (nextFocus != null)
                nextFocus.Focus();
            else if (UpdateRegisterButton())
                ClickRegistrar();
        }

        private async void ClickRegistrar()
        {
            if (BlockRegister)
                return;

            BlockRegister = true;
            Unfocus();
            var result = await Controllers.UserController.RegisterUser(entryUsername.Text.Trim(), entryEmail.Text.Trim(), entryPassword.Text.Trim());
            if (result != UsuarioResultEnum.Ok)
            {
                BlockRegister = false;
                DependencyService.Get<Interfaces.IToastService>().SendToast(result.GetText());
            }
            else
            {
                DependencyService.Get<Interfaces.IToastService>().SendToast(Resx.AppResources.RegisterOkMsg);
                await Task.Delay(2000);
                _ = Navigation.PopAsync();
            }
        }

        private bool UpdateRegisterButton()
        {
            var canEnable = true;
            if (string.IsNullOrEmpty(entryUsername.Text) || entryUsername.Text.Length < 3)
                canEnable = false;
            if (string.IsNullOrEmpty(entryEmail.Text) || !Util.IsEmailValid(entryEmail.Text))
                canEnable = false;
            if (string.IsNullOrEmpty(entryPassword.Text) || entryPassword.Text.Length < 8)
                canEnable = false;
            if (string.IsNullOrEmpty(entryPasswordRetype.Text))
                canEnable = false;
            if (entryPassword.Text != entryPasswordRetype.Text)
                canEnable = false;

            _viewModel.EnableRegister = canEnable;
            return canEnable;
        }

        void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRegisterButton();
        }
    }

    internal class RegisterPageViewModel : BindableObject
    {
        private bool _enableRegister;
        public bool EnableRegister
        {
            get
            {
                return _enableRegister;
            }
            set
            {
                _enableRegister = value;
                OnPropertyChanged();
            }
        }
    }
}
