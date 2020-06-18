using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Interfaces;
using GamesViewer_Xamarin.Models;
using SQLite;

namespace GamesViewer_Xamarin.Services
{
    public class SQLiteUsuarioJuegosService : IUsuarioJuegosService
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
                if (connection.TableMappings.Any(t => t.MappedType.Name == typeof(UsuarioJuegosModel).Name) == false)
                    await connection.CreateTablesAsync(CreateFlags.None, typeof(UsuarioJuegosModel)).ConfigureAwait(false);

                wasInitialized = true;
            }
        }

        public async Task<JuegoFavModel> GetJuegoFavByUserIdAndGameId(int userId, int gameId)
        {
            var query = await connection.QueryAsync<JuegoFavModel>($@"SELECT id, name, backgroundImage, releaseDate FROM juegofav
               INNER JOIN usuariosjuegos
               ON juegofav.id = usuariosjuegos.juegoId
               WHERE usuariosjuegos.usuarioId = {userId} AND usuariosJuegos.juegoId = {gameId})");
            return query.FirstOrDefault();
        }

        public async Task<List<JuegoFavModel>> GetJuegoFavsByUserId(int userId)
        {
            var query = await connection.QueryAsync<JuegoFavModel>($@"SELECT id, name, backgroundImage, releaseDate FROM juegofav
               INNER JOIN usuariosjuegos
               ON juegofav.id = usuariosjuegos.juegoId
               WHERE usuariosjuegos.usuarioId = {userId}");
            return query;
        }

        public async Task<bool> InsertJuegoFav(UsuarioJuegosModel model)
        {
            if (await connection.InsertAsync(model) > 0)
                return true;

            return false;
        }

        public async Task<bool> DeleteByUserIdAndGameId(UsuarioJuegosModel model)
        {
            if (await connection.DeleteAsync(model) > 0)
                return true;

            return false;
        }
    }
}
