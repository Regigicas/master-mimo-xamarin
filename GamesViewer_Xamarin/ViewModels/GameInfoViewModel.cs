using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    class GameInfoViewModel : BindableObject
    {
        private Models.Juego _juego;
        public Models.Juego Juego
        {
            get
            {
                return _juego;
            }
            set
            {
                _juego = value;
                OnPropertyChanged();
            }
        }

        private string _favIcon;
        public string FavIcon
        {
            get
            {
                return _favIcon;
            }
            set
            {
                _favIcon = value;
                OnPropertyChanged();
            }
        }

        private int _juegoId;
        public int JuegoId
        {
            get
            {
                return _juegoId;
            }
            set
            {
                _juegoId = value;
                UpdateGameData();
            }
        }

        private bool Initialized { get; set; }
        public ICommand QRCommand { get; private set; }
        public ICommand ToggleFavoriteCommand { get; private set; }

        public GameInfoViewModel()
        {
            FavIcon = "fav_status_off.png";
            Initialized = false;

            QRCommand = new Command(execute: () =>
            {
                var navigationService = new Services.NavigationService();
                navigationService.GoToQR(Juego);
            });

            ToggleFavoriteCommand = new Command(execute: () =>
            {
                if (!Initialized)
                    return;

                var userService = new Services.UserService();
                if (FavIcon == "fav_status_on.png")
                {
                    FavIcon = "fav_status_off.png";
                    userService.RemoveFavorite(JuegoId);
                }
                else
                {
                    FavIcon = "fav_status_on.png";
                    userService.AddFavorite(Juego);
                }
            });
        }

        private async void UpdateGameData()
        {
            var juegoService = new Services.JuegoService();
            var result = await juegoService.GetJuegoById(JuegoId);
            Juego = result;
            var userService = new Services.UserService();
            FavIcon = await userService.HasFavorite(JuegoId, null) ? "fav_status_on.png" : "fav_status_off.png";
            Initialized = true;
        }
    }
}
