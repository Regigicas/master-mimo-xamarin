
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GamesViewer_Xamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
