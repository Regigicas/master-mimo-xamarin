using System;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Models;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IJuegoFavDataService
    {
        Task<JuegoFavModel> GetJuegoFav(int id);
        Task<bool> InsertJuegoFav(JuegoFavModel model);
    }
}
