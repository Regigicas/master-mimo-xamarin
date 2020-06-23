using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    class FavoritePageViewModel : BindableObject
    {
        private List<Models.JuegoFav> _juegos = new List<Models.JuegoFav>();
        public List<Models.JuegoFav> Juegos
        {
            get
            {
                return _juegos;
            }
            set
            {
                _juegos = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionChangedCommand { get; private set; }

        public FavoritePageViewModel()
        {
            SelectionChangedCommand = new Command<CollectionView>(execute: (CollectionView collectionView) =>
            {
                if (collectionView.SelectedItem == null)
                    return;

                var navigationService = new Services.NavigationService();
                navigationService.GoToGame(((Models.JuegoFav)collectionView.SelectedItem).Id);
                collectionView.SelectedItem = null;
            });
        }

        public async Task ReloadFavData()
        {
            var userService = new Services.UserService();
            var results = await userService.GetFavorites();
            Juegos = results;
        }
    }
}
