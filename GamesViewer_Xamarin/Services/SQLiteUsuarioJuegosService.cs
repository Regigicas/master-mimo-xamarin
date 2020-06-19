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

        public async Task<Models.JuegoFav> GetJuegoFavByUserIdAndGameId(int userId, int gameId)
        {
            var query = await connection.QueryAsync<Models.JuegoFav>($@"SELECT id, name, backgroundImage, releaseDate FROM juegofav
               INNER JOIN usuariosjuegos
               ON juegofav.id = usuariosjuegos.juegoId
               WHERE usuariosjuegos.usuarioId = {userId} AND usuariosJuegos.juegoId = {gameId})");
            return query.FirstOrDefault();
        }

        public async Task<List<Models.JuegoFav>> GetJuegoFavsByUserId(int userId)
        {
            var query = await connection.QueryAsync<Models.JuegoFav>($@"SELECT id, name, backgroundImage, releaseDate FROM juegofav
               INNER JOIN usuariosjuegos
               ON juegofav.id = usuariosjuegos.juegoId
               WHERE usuariosjuegos.usuarioId = {userId}");
            return query;
        }

        public async Task<bool> InsertJuegoFav(Models.UsuarioJuegos model)
        {
            if (await connection.InsertAsync(model) > 0)
                return true;

            return false;
        }

        public async Task<bool> DeleteByUserIdAndGameId(Models.UsuarioJuegos model)
        {
            if (await connection.DeleteAsync(model) > 0)
                return true;

            return false;
        }
    }
}
