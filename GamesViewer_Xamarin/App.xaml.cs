using GamesViewer_Xamarin.Pages;
using Xamarin.Forms;

namespace GamesViewer_Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
