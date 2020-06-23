using Xamarin.Forms;

namespace GamesViewer_Xamarin.Services
{
    public class NavigationService
    {
        public async void GoToGame(int id)
        {
            var gameInfo = new Pages.GameInfo()
            {
                JuegoId = id
            };
            var tabPage = (TabbedPage)((NavigationPage)Application.Current.MainPage).CurrentPage;
            var tabNav = tabPage.CurrentPage.Navigation;
            await tabNav.PushAsync(gameInfo);
        }

        public async void GoToPlatform(int id)
        {
            var platformInfo = new Pages.PlatformInfo()
            {
                PlatformId = id
            };
            var tabPage = (TabbedPage)((NavigationPage)Application.Current.MainPage).CurrentPage;
            var tabNav = tabPage.CurrentPage.Navigation;
            await tabNav.PushAsync(platformInfo);
        }

        public async void GoToPlatformGames(Models.Platform platform)
        {
            var platformInfo = new Pages.PlatformGameList()
            {
                PassedPlatform = platform
            };
            var tabPage = (TabbedPage)((NavigationPage)Application.Current.MainPage).CurrentPage;
            var tabNav = tabPage.CurrentPage.Navigation;
            await tabNav.PushAsync(platformInfo);
        }

        public void GoToLogin()
        {
            Application.Current.MainPage = new NavigationPage(new Pages.LoginPage());
        }

        public async void GoToRegister()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Pages.RegisterPage());
        }

        public void GoToHome()
        {
            Application.Current.MainPage = new NavigationPage(new Pages.HomePage());
        }

        public async void GoToQR(Models.Juego juego)
        {
            var qrPage = new Pages.QRPage()
            {
                Juego = juego
            };
            var tabPage = (TabbedPage)((NavigationPage)Application.Current.MainPage).CurrentPage;
            var tabNav = tabPage.CurrentPage.Navigation;
            await tabNav.PushAsync(qrPage);
        }

        public async void GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async void GoToChangePasswordPage()
        {
            var tabPage = (TabbedPage)((NavigationPage)Application.Current.MainPage).CurrentPage;
            var tabNav = tabPage.CurrentPage.Navigation;
            await tabNav.PushAsync(new Pages.ChangePasswordPage());
        }
    }
}
