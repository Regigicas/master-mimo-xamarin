using System.Threading.Tasks;
using Refit;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IJuegosRequest
    {
        [Get("/games")]
        Task<Models.Juego.ResponseQuery> GetJuegos(int page, string ordering, int page_size = 20);
        [Get("/games")]
        Task<Models.Juego.ResponseQuery> GetJuegosPlatform(int page, int platforms, int page_size = 20);
        [Get("/games")]
        Task<Models.Juego.ResponseQuery> GetJuegosQuery(int page, string search, int page_size = 20);
        [Get("/games/{id}")]
        Task<Models.Juego> GetJuego(int id);
    }
}
