using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IUsuarioJuegosService
    {
        Task<List<Models.JuegoFav>> GetJuegoFavsByUserId(int userId);
        Task<Models.UsuarioJuegos> GetJuegoFavByUserIdAndGameId(int userId, int gameId);
        Task<bool> InsertJuegoFav(Models.UsuarioJuegos model);
        Task<bool> DeleteByUserIdAndGameId(Models.UsuarioJuegos model);
    }
}
