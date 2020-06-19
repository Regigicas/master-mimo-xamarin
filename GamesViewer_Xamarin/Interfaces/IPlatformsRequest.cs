using System.Threading.Tasks;
using Refit;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IPlatformsRequest
    {
        [Get("/platforms")]
        Task<Models.Platform.AllPlatformsResponse> GetPlatforms();
    }
}
