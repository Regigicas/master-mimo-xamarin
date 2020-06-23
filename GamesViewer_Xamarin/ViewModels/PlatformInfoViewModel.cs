using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    class PlatformInfoViewModel : BindableObject
    {
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

        private int _platformId;
        public int PlatformId
        {
            get
            {
                return _platformId;
            }
            set
            {
                _platformId = value;
                UpdatePageData();
            }
        }

        public ICommand GoPlatformCommand { get; internal set; }

        public PlatformInfoViewModel()
        {
            GoPlatformCommand = new Command(execute: () =>
            {
                var navigationService = new Services.NavigationService();
                navigationService.GoToPlatformGames(Platform);
            });
        }

        private async void UpdatePageData()
        {
            var platformService = new Services.PlatformService();
            var result = await platformService.GetPlatformInfo(PlatformId);
            Platform = result;
        }
    }
}
