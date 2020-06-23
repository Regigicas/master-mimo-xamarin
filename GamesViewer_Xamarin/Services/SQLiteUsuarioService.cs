using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(GamesViewer_Xamarin.Services.SQLiteUsuarioService))]
namespace GamesViewer_Xamarin.Services
{
    public class SQLiteUsuarioService : Interfaces.IUserDataService
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
                if (connection.TableMappings.Any(t => t.MappedType.Name == typeof(Models.Usuario).Name) == false)
                    await connection.CreateTablesAsync(CreateFlags.None, typeof(Models.Usuario)).ConfigureAwait(false);

                wasInitialized = true;
            }
        }

        public async Task<Models.Usuario> GetUser(int id)
        {
            return await connection.Table<Models.Usuario>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Models.Usuario> GetUserByUsername(string username)
        {
            return await connection.Table<Models.Usuario>().Where(u => u.Username.ToUpper() == username.ToUpper()).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertUser(Models.Usuario model)
        {
            if (await connection.InsertAsync(model) > 0)
                return true;

            return false;
        }

        public async Task<bool> UpdateUser(Models.Usuario model)
        {
            if (await connection.UpdateAsync(model) > 0)
                return true;

            return false;
        }

        public async Task<Models.Usuario> GetUserByEmail(string email)
        {
            return await connection.Table<Models.Usuario>().Where(u => u.Email.ToUpper() == email.ToUpper()).FirstOrDefaultAsync();
        }
    }
}
