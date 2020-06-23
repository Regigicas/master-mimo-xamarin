using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Services
{
    public class PlatformService
    {
        public async Task<Models.Platform.AllPlatformsResponse> GetAllPlatforms()
        {
            var controller = Refit.RestService.For<Interfaces.IPlatformsRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetPlatforms();
                return result;
            }
            catch
            {
                var toastService = new ToastService();
                toastService.SendToast(Resx.AppResources.RefitError);
            }

            var dummy = new Models.Platform.AllPlatformsResponse
            {
                Results = new List<Models.Platform>()
            };
            return dummy;
        }

        public async Task<Models.Platform> GetPlatformInfo(int id)
        {
            var controller = Refit.RestService.For<Interfaces.IPlatformsRequest>(Resx.AppResources.ApiURL);
            try
            {
                var result = await controller.GetPlatform(id);
                if (result != null && Device.RuntimePlatform == Device.iOS) // El parser de HTML no esta funcionando en iOS
                    result.Description = Util.HtmlToPlainText(result.Description);
                return result;
            }
            catch
            {
                var toastService = new ToastService();
                toastService.SendToast(Resx.AppResources.RefitError);
            }

            return null;
        }
    }
}
