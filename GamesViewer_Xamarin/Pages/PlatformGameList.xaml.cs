using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class PlatformGameList : ContentPage
    {
        private PlatformGameListViewModel _viewModel;
        private int LoadedElementCount { get; set; } = 0;
        private int MaxLoadedElements { get; set; } = 100;
        private int Page { get; set; } = 1;
        public Models.Platform _passedPlatForm;
        public Models.Platform PassedPlatform
        {
            get
            {
                return _passedPlatForm;
            }
            set
            {
                _passedPlatForm = value;
                _ = PopulateData(false);
            }
        }

        public PlatformGameList()
        {
            InitializeComponent();
            _viewModel = new PlatformGameListViewModel(this);
            BindingContext = _viewModel;
        }

        public async Task PopulateData(bool more)
        {
            if (more)
            {
                ++Page;
                LoadedElementCount += 20;
            }
            else
            {
                Page = 1;
                LoadedElementCount = 20;
                _viewModel.Platform = PassedPlatform;
            }

            var controller = Refit.RestService.For<Interfaces.IJuegosRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetJuegosPlatform(Page, PassedPlatform.Id);
                if (!more)
                    _viewModel.Juegos.Clear();
                _viewModel.AddAll(result.Results);
            }
            catch
            {
                DependencyService.Get<Interfaces.IToastService>().SendToast(Resx.AppResources.RefitError);
            }
        }

        void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedGame = e.SelectedItem as Models.Juego;
            ((ListView)sender).SelectedItem = null;
            var gameInfo = new GameInfo()
            {
                JuegoId = selectedGame.Id
            };
            Navigation.PushAsync(gameInfo);
        }

        async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (_viewModel.IsRefreshing)
                return;

            if (e.Item == _viewModel.Juegos.Last() && LoadedElementCount < MaxLoadedElements)
            {
                _viewModel.IsRefreshing = true;
                await PopulateData(true);
                _viewModel.IsRefreshing = false;
            }
        }

        internal class PlatformGameListViewModel : BindableObject
        {
            private PlatformGameList _owner;
            public ICommand RefreshCommand { get; private set; }
            private ObservableCollection<Models.Juego> _juegos = new ObservableCollection<Models.Juego>();
            public ObservableCollection<Models.Juego> Juegos
            {
                get
                {
                    return _juegos;
                }
                set { }
            }

            private Models.Platform _platform;
            public Models.Platform Platform
            {
                get
                {
                    return _platform;
                }
                set
                {
                    _platform = value;
                    OnPropertyChanged();
                }
            }

            private bool _isRefreshing;
            public bool IsRefreshing
            {
                get
                {
                    return _isRefreshing;
                }
                set
                {
                    _isRefreshing = value;
                    OnPropertyChanged();
                }
            }

            public PlatformGameListViewModel(PlatformGameList owner)
            {
                _owner = owner;
                IsRefreshing = false;
                RefreshCommand = new Command(async () =>
                {
                    IsRefreshing = true;
                    await _owner.PopulateData(false);
                    IsRefreshing = false;
                });
            }

            public void AddGame(Models.Juego model)
            {
                _juegos.Add(model);
            }

            public void AddAll(List<Models.Juego> juegos)
            {
                foreach (var juego in juegos)
                    _juegos.Add(juego);
            }
        }
    }
}
