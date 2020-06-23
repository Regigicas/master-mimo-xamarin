using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class PlatformInfo : ContentPage
    {
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
        }

        private void UpdatePageData()
        {
            ((ViewModels.PlatformInfoViewModel)BindingContext).PlatformId = PlatformId;
        }
    }
}
