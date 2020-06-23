using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class GameInfo : ContentPage
    {
        private int _juegoId;
        public int JuegoId
        {
            get
            {
                return _juegoId;
            }
            set
            {
                _juegoId = value;
                UpdatePageData();
            }
        }

        public GameInfo()
        {
            InitializeComponent();
        }

        private void UpdatePageData()
        {
            ((ViewModels.GameInfoViewModel)BindingContext).JuegoId = JuegoId;
        }
    }
}
