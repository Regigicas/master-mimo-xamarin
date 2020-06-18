using System;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Models;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IUserDataService
    {
        Task<UsuarioModel> GetUser(int id);
        Task<UsuarioModel> GetUserByUsername(string username);
        Task<bool> InsertUser(UsuarioModel model);
        Task<bool> UpdateUser(UsuarioModel model);
    }
}
