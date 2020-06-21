using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IJuegoFavDataService
    {
        Task<Models.JuegoFav> GetJuegoFav(int id);
        Task<bool> InsertJuegoFav(Models.JuegoFav model);
        Task<List<Models.JuegoFav>> GetJuegosFavByList(List<int> ids);
    }
}
