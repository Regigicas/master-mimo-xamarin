using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class FavoritePage : ContentPage
    {
        private bool InitialLoad { get; set; } = true;

        public FavoritePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!InitialLoad)
                _ = ((ViewModels.FavoritePageViewModel)BindingContext).ReloadFavData();
        }

        public async void ReloadFavData()
        {
            await ((ViewModels.FavoritePageViewModel)BindingContext).ReloadFavData();
            if (InitialLoad)
            {
                InitialLoad = false;
                await collectionView.ScaleTo(1.0, 1000, Easing.CubicIn);
            }
        }
    }
}
