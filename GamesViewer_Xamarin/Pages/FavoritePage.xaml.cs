using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class FavoritePage : ContentPage
    {
        private FavoritePageViewModel _viewModel = new FavoritePageViewModel();
        private bool InitialLoad { get; set; } = true;

        public FavoritePage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!InitialLoad)
                ReloadFavData();
        }

        public async void ReloadFavData()
        {
            var results = await Controllers.UserController.GetFavorites();
            _viewModel.Juegos = results;
            if (InitialLoad)
            {
                InitialLoad = false;
                await collectionView.ScaleTo(1.0, 1000, Easing.CubicIn);
            }
        }

        internal class FavoritePageViewModel : BindableObject
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
        }

        void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var selectedGame = e.CurrentSelection[0] as Models.JuegoFav;
            ((CollectionView)sender).SelectedItem = null;
            var gameInfo = new GameInfo()
            {
                JuegoId = selectedGame.Id
            };
            Navigation.PushAsync(gameInfo);
        }
    }
}
