using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    public class HomePageViewModel : BindableObject
    {
        public ICommand PageChangedCommand { get; private set; }

        public HomePageViewModel()
        {
            PageChangedCommand = new Command<TabbedPage>(execute: (TabbedPage tab) =>
            {
                if (tab.CurrentPage != null)
                    tab.Title = tab.CurrentPage.Title;

                if (tab.CurrentPage is NavigationPage navPage)
                    if (navPage.CurrentPage is Pages.FavoritePage page)
                        page.ReloadFavData();
            });
        }
    }
}
