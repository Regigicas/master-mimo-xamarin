using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class SearchPage : ContentPage
    {
        private SearchPageViewModel _viewModel;
        private int LoadedElementCount { get; set; } = 0;
        private int MaxLoadedElements { get; set; } = 100;
        private int Page { get; set; } = 1;

        public SearchPage()
        {
            InitializeComponent();
            _viewModel = new SearchPageViewModel(this);
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
            }

            var controller = Refit.RestService.For<Interfaces.IJuegosRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetJuegosQuery(Page, _viewModel.SearchQuery);
                if (!more)
                    _viewModel.JuegosResult.Clear();
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

            if (e.Item == _viewModel.JuegosResult.Last() && LoadedElementCount < MaxLoadedElements)
            {
                _viewModel.IsRefreshing = true;
                await PopulateData(true);
                _viewModel.IsRefreshing = false;
            }
        }

        async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var text = ((SearchBar)sender).Text;
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                return;

            _viewModel.SearchQuery = text;
            _viewModel.IsRefreshing = true;
            await PopulateData(false);
            _viewModel.IsRefreshing = false;
        }

        internal class SearchPageViewModel : BindableObject
        {
            private SearchPage _owner;
            public ICommand RefreshCommand { get; private set; }
            private ObservableCollection<Models.Juego> _juegosResult = new ObservableCollection<Models.Juego>();
            public ObservableCollection<Models.Juego> JuegosResult
            {
                get
                {
                    return _juegosResult;
                }
                set {}
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

            public string SearchQuery { get; set; }

            public SearchPageViewModel(SearchPage owner)
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
                _juegosResult.Add(model);
            }

            public void AddAll(List<Models.Juego> juegos)
            {
                foreach (var juego in juegos)
                    _juegosResult.Add(juego);
            }
        }
    }
}
