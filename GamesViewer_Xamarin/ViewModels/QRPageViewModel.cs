using Newtonsoft.Json;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.ViewModels
{
    class QRPageViewModel : BindableObject
    {
        private string _barCodeValue;
        public string BarCodeValue
        {
            get
            {
                return _barCodeValue;
            }
            set
            {
                _barCodeValue = value;
                OnPropertyChanged();
            }
        }

        private Models.Juego _juego;
        public Models.Juego Juego
        {
            get
            {
                return _juego;
            }
            set
            {
                _juego = value;
                GenerateQRValue();
                OnPropertyChanged();
            }
        }

        void GenerateQRValue()
        {
            var qrModel = new Models.QR()
            {
                Id = Juego.Id,
                Name = Juego.Name
            };

            BarCodeValue = JsonConvert.SerializeObject(qrModel);
        }
    }
}
