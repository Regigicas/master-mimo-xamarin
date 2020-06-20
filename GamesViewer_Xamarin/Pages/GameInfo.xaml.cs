using System;
using System.Collections.Generic;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class GameInfo : ContentPage
    {
        private GameInfoViewModel _viewModel = new GameInfoViewModel();
        private bool Initialized { get; set; } = false;
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
                UpdatePageData();
            }
        }

        public GameInfo()
        {
            InitializeComponent();
            _viewModel.FavIcon = "fav_status_off.png";
            BindingContext = _viewModel;
        }

        private async void UpdatePageData()
        {
            var controller = Refit.RestService.For<Interfaces.IJuegosRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetJuego(JuegoId);
                if (result != null && Device.RuntimePlatform == Device.iOS) // El parser de HTML no esta funcionando en iOS
                {
                    labelDesc.TextType = TextType.Text;
                    result.Description = Util.HtmlToPlainText(result.Description);
                }
                _viewModel.Juego = result;
                _viewModel.FavIcon = await Controllers.UserController.HasFavorite(JuegoId, null) ? "fav_status_on.png" : "fav_status_off.png";
                Initialized = true;
            }
            catch
            {
                DependencyService.Get<Interfaces.IToastService>().SendToast(Resx.AppResources.RefitError);
            }
        }

        void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (!Initialized)
                return;

            if (_viewModel.FavIcon == "fav_status_on.png")
            {
                _viewModel.FavIcon = "fav_status_off.png";
                Controllers.UserController.RemoveFavorite(JuegoId);
            }
            else
            {
                _viewModel.FavIcon = "fav_status_on.png";
                Controllers.UserController.AddFavorite(_viewModel.Juego);
            }
        }

        void ToolbarItem_Clicked_QR(object sender, EventArgs e)
        {

        }
    }

    internal class GameInfoViewModel : BindableObject
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
    }
}
