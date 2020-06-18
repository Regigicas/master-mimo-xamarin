using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Interfaces;
using GamesViewer_Xamarin.Models;
using SQLite;

namespace GamesViewer_Xamarin.Services
{
    public class SQLiteUsuarioService : IUserDataService
    {
        static SQLiteAsyncConnection connection;
        static bool wasInitialized = false;
        public SQLiteUsuarioService()
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string databaseFileName = "UserData.db3";
            connection = new SQLiteAsyncConnection(Path.Combine(basePath, databaseFileName), SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
            Initialize();
        }

        private async void Initialize()
        {
            if (wasInitialized == false)
            {
                if (connection.TableMappings.Any(t => t.MappedType.Name == typeof(UsuarioModel).Name) == false)
                    await connection.CreateTablesAsync(CreateFlags.None, typeof(UsuarioModel)).ConfigureAwait(false);

                wasInitialized = true;
            }
        }

        public async Task<UsuarioModel> GetUser(int id)
        {
            return await connection.Table<UsuarioModel>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UsuarioModel> GetUserByUsername(string username)
        {
            return await connection.Table<UsuarioModel>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertUser(UsuarioModel model)
        {
            if (await connection.InsertAsync(model) > 0)
                return true;

            return false;
        }

        public async Task<bool> UpdateUser(UsuarioModel model)
        {
            if (await connection.UpdateAsync(model) > 0)
                return true;

            return false;
        }
    }
}
