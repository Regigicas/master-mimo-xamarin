using System.Threading.Tasks;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IUserDataService
    {
        Task<Models.Usuario> GetUser(int id);
        Task<Models.Usuario> GetUserByUsername(string username);
        Task<Models.Usuario> GetUserByEmail(string email);
        Task<bool> InsertUser(Models.Usuario model);
        Task<bool> UpdateUser(Models.Usuario model);
    }
}
