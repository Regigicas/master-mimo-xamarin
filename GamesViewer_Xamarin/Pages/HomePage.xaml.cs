
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

        void TabbedPage_CurrentPageChanged(object sender, System.EventArgs e)
        {
            if (CurrentPage != null)
                Title = CurrentPage.Title;

            if (CurrentPage is NavigationPage navPage)
                if (navPage.CurrentPage is FavoritePage page)
                    page.ReloadFavData();
        }
    }
}
