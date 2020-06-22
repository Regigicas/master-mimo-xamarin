using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Misc;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Controllers
{
    public static class UserController
    {
        static string UsernameStoreFieldName = "acc_login_username";
        static string PasswordStoreFieldname = "acc_login_password";
        static string ActiveUserId = "acc_active_user_id";

        public static async Task<bool> TryAutoLogin()
        {
            var secureStorage = DependencyService.Get<Interfaces.ISecureStorage>();
            var username = await secureStorage.GetPropertyAsync(UsernameStoreFieldName);
            var password = await secureStorage.GetPropertyAsync(PasswordStoreFieldname);
            if (username != null && password != null)
            {
                var loginResult = await TryLogin(username, password);
                if (loginResult.Item1 == Enums.UsuarioResultEnum.Ok)
                {
                    SaveActiveUserId(loginResult.Item2.Id);
                    return true;
                }
            }

            return false;
        }

        public static async Task<Enums.UsuarioResultEnum> RegisterUser(string username, string email, string password)
        {
            if (!Util.IsEmailValid(email))
                return Enums.UsuarioResultEnum.InvalidEmail;

            var passHash = Util.HashPassword($"{username.ToUpper()}:{password}");
            var usuarioDB = DependencyService.Get<Interfaces.IUserDataService>();
            var checkUserName = await usuarioDB.GetUserByUsername(username);
            if (checkUserName != null)
                return Enums.UsuarioResultEnum.ExistingUser;

            var checkEmail = await usuarioDB.GetUserByEmail(email);
            if (checkEmail != null)
                return Enums.UsuarioResultEnum.ExistingEmail;

            var usuario = new Models.Usuario
            {
                Username = username,
                Email = email,
                ShaHashPass = passHash,
                FavoriteNotification = false
            };
            if (!await usuarioDB.InsertUser(usuario))
                return Enums.UsuarioResultEnum.InternalError;

            return Enums.UsuarioResultEnum.Ok;
        }

        public static async Task<Tuple<Enums.UsuarioResultEnum, Models.Usuario>> TryLogin(string username, string password)
        {
            var usuarioDB = DependencyService.Get<Interfaces.IUserDataService>();
            var usuario = await usuarioDB.GetUserByUsername(username);
            if (usuario == null)
                return Tuple.Create(Enums.UsuarioResultEnum.UsernameNotFound, (Models.Usuario)null);

            var passHash = Util.HashPassword($"{username.ToUpper()}:{password}");
            if (usuario.ShaHashPass != passHash)
                return Tuple.Create(Enums.UsuarioResultEnum.PasswordMismatch, (Models.Usuario)null);

            return Tuple.Create(Enums.UsuarioResultEnum.Ok, usuario);
        }

        public static async Task<bool> SaveUserLoginDataAsync(string username, string password)
        {
            var secureStorage = DependencyService.Get<Interfaces.ISecureStorage>();
            var result = await secureStorage.SetPropertyAsync(UsernameStoreFieldName, username);
            if (result)
                result = await secureStorage.SetPropertyAsync(PasswordStoreFieldname, password);
            return result;
        }

        public static void SaveActiveUserId(int id)
        {
            Preferences.Set(ActiveUserId, id);
        }

        public static async Task<Models.Usuario> GetUsuarioActivo()
        {
            int userId = Preferences.Get(ActiveUserId, -1);
            if (userId == -1)
                return null;

            var usuarioDB = DependencyService.Get<Interfaces.IUserDataService>();
            return await usuarioDB.GetUser(userId);
        }

        public static async Task<bool> HasFavorite(int juegoId, Models.Usuario usuarioActivo)
        {
            if (usuarioActivo == null)
                usuarioActivo = await GetUsuarioActivo();
            if (usuarioActivo == null)
                return false;

            var userFavsDB = DependencyService.Get<Interfaces.IUsuarioJuegosService>();
            var result = await userFavsDB.GetJuegoFavByUserIdAndGameId(usuarioActivo.Id, juegoId);
            return result != null;
        }

        public static async void AddFavorite(Models.Juego juego)
        {
            var usuarioActivo = await GetUsuarioActivo();
            if (usuarioActivo == null)
                return;

            if (await HasFavorite(juego.Id, usuarioActivo))
                return;

            var juegoFav = await JuegoController.GetJuegoFav(juego.Id);
            if (juegoFav == null)
                juegoFav = await JuegoController.InsertJuegoFav(juego);
            if (juegoFav == null)
                return;

            var userJuego = new Models.UsuarioJuegos()
            {
                UsuarioId = usuarioActivo.Id,
                JuegoId = juegoFav.Id
            };

            var userFavsDB = DependencyService.Get<Interfaces.IUsuarioJuegosService>();
            await userFavsDB.InsertJuegoFav(userJuego);
        }

        public static async void RemoveFavorite(int juegoId)
        {
            var usuarioActivo = await GetUsuarioActivo();
            if (usuarioActivo == null)
                return;

            if (!await HasFavorite(juegoId, usuarioActivo))
                return;

            var userJuego = new Models.UsuarioJuegos()
            {
                UsuarioId = usuarioActivo.Id,
                JuegoId = juegoId
            };

            var userFavsDB = DependencyService.Get<Interfaces.IUsuarioJuegosService>();
            await userFavsDB.DeleteByUserIdAndGameId(userJuego);
        }

        public static async Task<List<Models.JuegoFav>> GetFavorites()
        {
            var usuarioActivo = await GetUsuarioActivo();
            if (usuarioActivo == null)
                return null;

            var userFavsDB = DependencyService.Get<Interfaces.IUsuarioJuegosService>();
            return await userFavsDB.GetJuegoFavsByUserId(usuarioActivo.Id);
        }

        public static async Task DoLogout()
        {
            Preferences.Remove(ActiveUserId);
            var secureStorage = DependencyService.Get<Interfaces.ISecureStorage>();
            await secureStorage.SetPropertyAsync(UsernameStoreFieldName, null);
            await secureStorage.SetPropertyAsync(PasswordStoreFieldname, null);
        }
    }
}
