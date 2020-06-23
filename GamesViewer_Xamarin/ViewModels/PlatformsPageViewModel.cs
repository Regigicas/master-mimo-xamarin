using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    class PlatformsPageViewModel : BindableObject
    {
        private ObservableCollection<Models.Platform> _platforms = new ObservableCollection<Models.Platform>();
        public ObservableCollection<Models.Platform> Platforms
        {
            get
            {
                return _platforms;
            }
            set {}
        }

        private Models.Platform _selectedItem;
        public Models.Platform SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                ItemSelected();
                OnPropertyChanged();
            }
        }

        public PlatformsPageViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            var platformService = new Services.PlatformService();
            var result = await platformService.GetAllPlatforms();
            foreach (var plat in result.Results)
                Platforms.Add(plat);
        }

        void ItemSelected()
        {
            if (SelectedItem == null)
                return;

            var navigationService = new Services.NavigationService();
            navigationService.GoToPlatform(SelectedItem.Id);
            SelectedItem = null;
        }
    }
}
