using System;
using System.Threading.Tasks;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface ISecureStorage
    {
        Task<string> GetPropertyAsync(string key);
        Task<bool> SetPropertyAsync(string key, string value);
    }
}
