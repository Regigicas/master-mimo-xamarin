using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Models;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IUsuarioJuegosService
    {
        Task<List<JuegoFavModel>> GetJuegoFavsByUserId(int userId);
        Task<JuegoFavModel> GetJuegoFavByUserIdAndGameId(int userId, int gameId);
        Task<bool> InsertJuegoFav(UsuarioJuegosModel model);
        Task<bool> DeleteByUserIdAndGameId(UsuarioJuegosModel model);
    }
}
