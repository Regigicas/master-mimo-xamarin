using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(GamesViewer_Xamarin.Services.SecureStorage))]
namespace GamesViewer_Xamarin.Services
{
    public class SecureStorage : Interfaces.ISecureStorage
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
            catch {}

            return false;
        }
    }
}
