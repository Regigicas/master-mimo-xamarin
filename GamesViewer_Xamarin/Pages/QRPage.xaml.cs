using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Pages
{
    public partial class QRPage : ContentPage
    {
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
                UpdateViewValue();
            }
        }

        public QRPage()
        {
            InitializeComponent();
        }

        public void UpdateViewValue()
        {
            ((ViewModels.QRPageViewModel)BindingContext).Juego = Juego;
            var animation = new Animation(v => imageView.Scale = v, 0.6, 1);
            animation.Commit(this, "ScaleAnimation", 16, 2000, Easing.Linear, (v, c) => imageView.Scale = 1, () => false);
        }
    }
}
