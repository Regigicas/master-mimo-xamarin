using System;
using System.Collections.Generic;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class PlatformInfo : ContentPage
    {
        private PlatformInfoViewModel _viewModel = new PlatformInfoViewModel();
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

        public PlatformInfo()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        private async void UpdatePageData()
        {
            var controller = Refit.RestService.For<Interfaces.IPlatformsRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetPlatform(PlatformId);
                if (result != null && Device.RuntimePlatform == Device.iOS) // El parser de HTML no esta funcionando en iOS
                {
                    labelDesc.TextType = TextType.Text;
                    result.Description = Util.HtmlToPlainText(result.Description);
                }
                _viewModel.Platform = result;
            }
            catch
            {
                DependencyService.Get<Interfaces.IToastService>().SendToast(Resx.AppResources.RefitError);
            }
        }

        void Button_Games_Clicked(object sender, EventArgs e)
        {
            var platformView = new PlatformGameList
            {
                PassedPlatform = _viewModel.Platform
            };
            Navigation.PushAsync(platformView);
        }
    }

    internal class PlatformInfoViewModel : BindableObject
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
    }
}
