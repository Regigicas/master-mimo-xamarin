using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class PlatformsPage : ContentPage
    {
        private PlatformsPageViewModel _viewModel = new PlatformsPageViewModel();

        public PlatformsPage()
        {
            InitializeComponent();
            LoadViewData();
            BindingContext = _viewModel;
        }

        private async void LoadViewData()
        {
            var controller = Refit.RestService.For<Interfaces.IPlatformsRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetPlatforms();
                _viewModel.Platforms = result.Results;
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

            var platform = e.SelectedItem as Models.Platform;
            ((ListView)sender).SelectedItem = null;
            var platformView = new PlatformInfo
            {
                PlatformId = platform.Id
            };
            Navigation.PushAsync(platformView);
        }

        internal class PlatformsPageViewModel : BindableObject
        {
            private List<Models.Platform> _platforms;
            public List<Models.Platform> Platforms
            {
                get
                {
                    return _platforms;
                }
                set
                {
                    _platforms = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
