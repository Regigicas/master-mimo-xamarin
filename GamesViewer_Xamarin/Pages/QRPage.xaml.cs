using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class QRPage : ContentPage
    {
        private QRPageViewModel _viewModel = new QRPageViewModel();
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
                _viewModel.Juego = value;
                GenerateQRValue();
            }
        }

        public QRPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        void GenerateQRValue()
        {
            var qrModel = new Models.QR()
            {
                Id = Juego.Id,
                Name = Juego.Name
            };

            _viewModel.BarCodeValue = JsonConvert.SerializeObject(qrModel);
            var animation = new Animation(v => imageView.Scale = v, 0.6, 1);
            animation.Commit(this, "ScaleAnimation", 16, 2000, Easing.Linear, (v, c) => imageView.Scale = 1, () => false);
        }

        internal class QRPageViewModel : BindableObject
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
                    OnPropertyChanged();
                }
            }
        }
    }
}
