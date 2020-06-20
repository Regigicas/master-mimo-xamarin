using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Controllers
{
    public static class JuegoController
    {
        public static async Task<Models.JuegoFav> GetJuegoFav(int id)
        {
            var juegoFavDB = DependencyService.Get<Interfaces.IJuegoFavDataService>();
            return await juegoFavDB.GetJuegoFav(id);
        }

        public static async Task<Models.JuegoFav> InsertJuegoFav(Models.Juego juego)
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
    }
}
