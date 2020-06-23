using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    internal class SearchPageViewModel : BindableObject
    {
        private int LoadedElementCount { get; set; } = 0;
        private int MaxLoadedElements { get; set; } = 100;
        private int Page { get; set; } = 1;
        public string SearchQuery { get; set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ItemAppearingCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        private bool IsLoadingMoreData { get; set; } = false;

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

        private Models.Juego _selectedGame;
        public Models.Juego SelectedGame
        {
            get
            {
                return _selectedGame;
            }
            set
            {
                _selectedGame = value;
                GoToGame();
                OnPropertyChanged();
            }
        }

        public SearchPageViewModel()
        {
            IsRefreshing = false;
            RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await PopulateData(false);
                IsRefreshing = false;
            });

            ItemAppearingCommand = new Command<Models.Juego>(execute: async (Models.Juego juego) =>
            {
                if (IsLoadingMoreData)
                    return;

                if (juego == JuegosResult.Last() && LoadedElementCount < MaxLoadedElements)
                {
                    IsLoadingMoreData = true;
                    await PopulateData(true);
                    IsLoadingMoreData = false;
                }
            });

            SearchCommand = new Command<string>(execute: async (string text) =>
            {
                if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                    return;

                SearchQuery = text;
                IsRefreshing = true;
                await PopulateData(false);
                IsRefreshing = false;
            });
        }

        public void GoToGame()
        {
            if (SelectedGame == null)
                return;

            var navigationService = new Services.NavigationService();
            navigationService.GoToGame(SelectedGame.Id);
            SelectedGame = null;
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

            var juegoService = new Services.JuegoService();
            var result = await juegoService.GetGamesSearch(Page, SearchQuery);
            if (!more)
                JuegosResult.Clear();
            AddAll(result.Results);
        }
    }
}
