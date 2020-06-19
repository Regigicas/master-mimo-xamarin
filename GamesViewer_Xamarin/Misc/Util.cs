using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace GamesViewer_Xamarin.Misc
{
    public static class Util
    {
        public static bool IsEmailValid(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        public static string HashPassword(string pass)
        {
            var crypt = new SHA256Managed();
            string hash = string.Empty;
            byte[] bytes = crypt.ComputeHash(Encoding.ASCII.GetBytes(pass));
            foreach (byte b in bytes)
                hash += b.ToString("X2");

            return hash;
        }
    }
}
