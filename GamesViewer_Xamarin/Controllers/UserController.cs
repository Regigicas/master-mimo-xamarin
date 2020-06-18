using System;
using System.Threading.Tasks;

namespace GamesViewer_Xamarin.Controllers
{
    public static class UserController
    {
        static string UsernameStoreFieldName = "acc_login_username";
        static string PasswordStoreFieldname = "acc_login_password";
        static string ActiveUserId = "acc_active_user_id";

        public static async Task<bool> tryAutoLogin()
        {
            return false;
        }
    }
}
