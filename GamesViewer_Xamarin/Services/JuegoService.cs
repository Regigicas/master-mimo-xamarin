using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Enums;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Services
{
    public class JuegoService
    {
        public async Task<Models.JuegoFav> GetJuegoFav(int id)
        {
            var juegoFavDB = DependencyService.Get<Interfaces.IJuegoFavDataService>();
            return await juegoFavDB.GetJuegoFav(id);
        }

        public async Task<Models.JuegoFav> InsertJuegoFav(Models.Juego juego)
        {
            var juegoFav = new Models.JuegoFav()
            {
                Id = juego.Id,
                Name = juego.Name,
                BackgroundImage = juego.BackgroundImage,
                ReleaseDate = DateTime.ParseExact(juego.Released, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
            };

            var juegoFavDB = DependencyService.Get<Interfaces.IJuegoFavDataService>();
            var result = await juegoFavDB.InsertJuegoFav(juegoFav);
            return result ? juegoFav : null;
        }

        public async Task<Models.Juego.ResponseQuery> GetGamesSearch(int page, string searchQuery)
        {
            var controller = Refit.RestService.For<Interfaces.IJuegosRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetJuegosQuery(page, searchQuery);
                return result;
            }
            catch
            {
                var toastService = new ToastService();
                toastService.SendToast(Resx.AppResources.RefitError);
            }

            var dummy = new Models.Juego.ResponseQuery
            {
                Results = new List<Models.Juego>()
            };
            return dummy;
        }

        public async Task<Models.Juego.ResponseQuery> GetGamesByPlatform(int page, int platform)
        {
            var controller = Refit.RestService.For<Interfaces.IJuegosRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetJuegosPlatform(page, platform);
                return result;
            }
            catch
            {
                var toastService = new ToastService();
                toastService.SendToast(Resx.AppResources.RefitError);
            }

            var dummy = new Models.Juego.ResponseQuery
            {
                Results = new List<Models.Juego>()
            };
            return dummy;
        }

        public async Task<Models.Juego> GetJuegoById(int id)
        {
            var controller = Refit.RestService.For<Interfaces.IJuegosRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetJuego(id);
                if (result != null && Device.RuntimePlatform == Device.iOS) // El parser de HTML no esta funcionando en iOS
                    result.Description = Util.HtmlToPlainText(result.Description);
                return result;
            }
            catch
            {
                var toastService = new ToastService();
                toastService.SendToast(Resx.AppResources.RefitError);
            }

            return null;
        }

        public async Task<Models.Juego.ResponseQuery> GetGamesOrder(int page, JuegoOrderEnum order)
        {
            var controller = Refit.RestService.For<Interfaces.IJuegosRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetJuegos(page, order.GetApiValue());
                return result;
            }
            catch
            {
                var toastService = new ToastService();
                toastService.SendToast(Resx.AppResources.RefitError);
            }

            var dummy = new Models.Juego.ResponseQuery
            {
                Results = new List<Models.Juego>()
            };
            return dummy;
        }
    }
}
