using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Interfaces;
using GamesViewer_Xamarin.Models;
using SQLite;

namespace GamesViewer_Xamarin.Services
{
    public class SQLiteJuegoFavService : IJuegoFavDataService
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
                if (connection.TableMappings.Any(t => t.MappedType.Name == typeof(JuegoFavModel).Name) == false)
                    await connection.CreateTablesAsync(CreateFlags.None, typeof(JuegoFavModel)).ConfigureAwait(false);

                wasInitialized = true;
            }
        }

        public async Task<JuegoFavModel> GetJuegoFav(int id)
        {
            return await connection.Table<JuegoFavModel>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertJuegoFav(JuegoFavModel model)
        {
            if (await connection.UpdateAsync(model) > 0)
                return true;

            return false;
        }
    }
}
