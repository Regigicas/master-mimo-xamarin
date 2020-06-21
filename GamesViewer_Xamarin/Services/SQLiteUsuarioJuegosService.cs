using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(GamesViewer_Xamarin.Services.SQLiteUsuarioJuegosService))]
namespace GamesViewer_Xamarin.Services
{
    public class SQLiteUsuarioJuegosService : Interfaces.IUsuarioJuegosService
    {
        static SQLiteAsyncConnection connection;
        static bool wasInitialized = false;

        public SQLiteUsuarioJuegosService()
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string databaseFileName = "UserJuegos.db3";
            connection = new SQLiteAsyncConnection(Path.Combine(basePath, databaseFileName), SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
            Initialize();
        }

        private async void Initialize()
        {
            if (wasInitialized == false)
            {
                if (connection.TableMappings.Any(t => t.MappedType.Name == typeof(Models.UsuarioJuegos).Name) == false)
                    await connection.CreateTablesAsync(CreateFlags.None, typeof(Models.UsuarioJuegos)).ConfigureAwait(false);

                wasInitialized = true;
            }
        }

        public async Task<Models.UsuarioJuegos> GetJuegoFavByUserIdAndGameId(int userId, int gameId)
        {
            return await connection.Table<Models.UsuarioJuegos>().Where(u => u.UsuarioId == userId && u.JuegoId == gameId).FirstOrDefaultAsync();
        }

        public async Task<List<Models.JuegoFav>> GetJuegoFavsByUserId(int userId)
        {
            var favs = await connection.Table<Models.UsuarioJuegos>().Where(u => u.UsuarioId == userId).ToListAsync();
            var juegoFavDB = DependencyService.Get<Interfaces.IJuegoFavDataService>();
            var ids = (from fav in favs
                       select fav.JuegoId).ToList();
            return await juegoFavDB.GetJuegosFavByList(ids);
        }

        public async Task<bool> InsertJuegoFav(Models.UsuarioJuegos model)
        {
            if (await connection.InsertAsync(model) > 0)
                return true;

            return false;
        }

        public async Task<bool> DeleteByUserIdAndGameId(Models.UsuarioJuegos model)
        {
            if (await connection.Table<Models.UsuarioJuegos>().Where(x => x.JuegoId == model.JuegoId).DeleteAsync() > 0)
                return true;

            return false;
        }
    }
}
