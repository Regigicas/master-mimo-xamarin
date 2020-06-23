using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GamesViewer_Xamarin.Enums;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    public class StartPageViewModel : BindableObject
    {
        public List<string> _pickerData;
        public List<string> PickerData
        {
            get
            {
                return _pickerData;
            }
            private set
            {
                _pickerData = value;
                OnPropertyChanged();
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
                _ = LoadData(false);
            }
        }

        private ObservableCollection<Models.Juego> _juegosResult = new ObservableCollection<Models.Juego>();
        public ObservableCollection<Models.Juego> Juegos
        {
            get
            {
                return _juegosResult;
            }
            set {}
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

        private bool _barScannerVisible;
        public bool BarScannerVisible
        {
            get
            {
                return _barScannerVisible;
            }
            set
            {
                _barScannerVisible = value;
                OnPropertyChanged();
            }
        }

        private int LoadedElementCount { get; set; } = 0;
        private int MaxLoadedElements { get; set; } = 100;
        private int Page { get; set; } = 1;
        private bool IsLoadingMoreData { get; set; } = false;
        public ICommand SelectionChangedCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ThresholdReachedCommand { get; private set; }
        public ICommand QRCommand { get; private set; }
        public ICommand ScanResultCommand { get; private set; }

        public StartPageViewModel()
        {
            BarScannerVisible = false;
            PickerData = Enum.GetValues(typeof(JuegoOrderEnum))
                .Cast<JuegoOrderEnum>().Select(v => v.GetStringValue()).ToList();

            SelectionChangedCommand = new Command<CollectionView>(execute: (CollectionView collectionView) =>
            {
                if (collectionView.SelectedItem == null)
                    return;

                var navigationService = new Services.NavigationService();
                navigationService.GoToGame(((Models.Juego)collectionView.SelectedItem).Id);
                collectionView.SelectedItem = null;
            });

            RefreshCommand = new Command(execute: async () =>
            {
                IsRefreshing = true;
                await LoadData(false);
                IsRefreshing = false;
            });

            ThresholdReachedCommand = new Command(execute: async () =>
            {
                if (LoadedElementCount >= MaxLoadedElements || IsLoadingMoreData)
                    return;

                IsLoadingMoreData = true;
                await LoadData(true);
                IsLoadingMoreData = false;
            });

            QRCommand = new Command(execute: () =>
            {
                BarScannerVisible = !BarScannerVisible;
            });

            ScanResultCommand = new Command<ZXing.Result>(execute: (ZXing.Result result) =>
            {
                if (result.Text == null || !BarScannerVisible)
                    return;

                var qrMode = JsonConvert.DeserializeObject<Models.QR>(result.Text);
                if (qrMode == null)
                    return;

                BarScannerVisible = false;
                Device.InvokeOnMainThreadAsync(() =>
                {
                    var navigationService = new Services.NavigationService();
                    navigationService.GoToGame(qrMode.Id);
                });
            });
        }

        public void AddAll(List<Models.Juego> juegos)
        {
            foreach (var juego in juegos)
                _juegosResult.Add(juego);
        }

        public async Task LoadData(bool more)
        {
            if (SelectedIndex == -1)
                return;

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

            var orders = (JuegoOrderEnum[])Enum.GetValues(typeof(JuegoOrderEnum));
            var juegoService = new Services.JuegoService();
            var result = await juegoService.GetGamesOrder(Page, orders[SelectedIndex]);
            if (!more)
                Juegos.Clear();
            AddAll(result.Results);
        }
    }
}
