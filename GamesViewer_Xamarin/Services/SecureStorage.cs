using System;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Interfaces;

namespace GamesViewer_Xamarin.Services
{
    public class SecureStorage : ISecureStorage
    {
        public async Task<string> GetPropertyAsync(string key)
        {
            try
            {
                return await Xamarin.Essentials.SecureStorage.GetAsync(key);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SetPropertyAsync(string key, string value)
        {
            try
            {
                await Xamarin.Essentials.SecureStorage.SetAsync(key, value);
                return true;
            }
            catch { }

            return false;
        }
    }
}
