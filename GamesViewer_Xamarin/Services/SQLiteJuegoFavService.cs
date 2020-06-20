using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(GamesViewer_Xamarin.Services.SQLiteJuegoFavService))]
namespace GamesViewer_Xamarin.Services
{
    public class SQLiteJuegoFavService : Interfaces.IJuegoFavDataService
    {
        static SQLiteAsyncConnection connection;
        static bool wasInitialized = false;
        public SQLiteJuegoFavService()
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string databaseFileName = "JuegoFavData.db3";
            connection = new SQLiteAsyncConnection(Path.Combine(basePath, databaseFileName), SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
            Initialize();
        }

        private async void Initialize()
        {
            if (wasInitialized == false)
            {
                if (connection.TableMappings.Any(t => t.MappedType.Name == typeof(Models.JuegoFav).Name) == false)
                    await connection.CreateTablesAsync(CreateFlags.None, typeof(Models.JuegoFav)).ConfigureAwait(false);

                wasInitialized = true;
            }
        }

        public async Task<Models.JuegoFav> GetJuegoFav(int id)
        {
            return await connection.Table<Models.JuegoFav>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertJuegoFav(Models.JuegoFav model)
        {
            if (await connection.InsertAsync(model) > 0)
                return true;

            return false;
        }
    }
}
