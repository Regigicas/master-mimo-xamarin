using System.Threading.Tasks;
using Refit;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IJuegosRequest
    {
        [Get("/games")]
        Task<Models.Juego.ResponseQuery> GetJuegos(int page);
    }
}
