using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class PlatformGameList : ContentPage
    {
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
                UpdateViewData();
            }
        }

        public PlatformGameList()
        {
            InitializeComponent();
        }

        void UpdateViewData()
        {
            ((ViewModels.PlatformGameListViewModel)BindingContext).Platform = PassedPlatform;
        }
    }
}
