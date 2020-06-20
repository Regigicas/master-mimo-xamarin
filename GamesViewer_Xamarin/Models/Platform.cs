using System.Collections.Generic;
using Newtonsoft.Json;

namespace GamesViewer_Xamarin.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonProperty(PropertyName = "image_background")]
        public string BackgroundImage { get; set; }
        [JsonIgnore]
        public string DescriptionWithPlaceholder
        {
            get
            {
                return string.IsNullOrEmpty(Description) ? Resx.AppResources.NoDescription : Description;
            }
            set {}
        }

        public class PlatformsResponse
        {
            public Platform Platform { get; set; }
        }

        public class AllPlatformsResponse
        {
            public List<Platform> Results { get; set; }
        }

        public string ImgFile // El API no devuelve el logo, pero si lo tiene en la pagina, asi que usamos está funcion
        {
            get
            {
                var imgPath = "logo_web";
                if (!string.IsNullOrEmpty(Name))
                {
                    var plataformaNombre = Name.ToLower();
                    if (plataformaNombre.Contains("pc"))
                        imgPath = "logo_pc";
                    else if (plataformaNombre.Contains("sega") || plataformaNombre.Contains("dreamcast") || plataformaNombre.Contains("game gear") ||
                        plataformaNombre.Contains("genesis") || plataformaNombre.Contains("nepnep"))
                        imgPath = "logo_sega";
                    else if (plataformaNombre.Contains("playstation") || plataformaNombre.Contains("ps"))
                        imgPath = "logo_ps";
                    else if (plataformaNombre.Contains("xbox"))
                        imgPath = "logo_xbox";
                    else if (plataformaNombre.Contains("nintendo") || plataformaNombre.Contains("gamecube") || plataformaNombre.Contains("game boy") ||
                        plataformaNombre.Contains("nes") || plataformaNombre.Contains("wii"))
                        imgPath = "logo_nintendo";
                    else if (plataformaNombre.Contains("atari") || plataformaNombre.Contains("jaguar"))
                        imgPath = "logo_atari";
                    else if (plataformaNombre.Contains("mac") || plataformaNombre.Contains("ios") || plataformaNombre.Contains("apple"))
                        imgPath = "logo_apple";
                    else if (plataformaNombre.Contains("android"))
                        imgPath = "logo_android";
                    else if (plataformaNombre.Contains("linux"))
                        imgPath = "logo_linux";
                    else if (plataformaNombre.Contains("commodore"))
                        imgPath = "logo_commodore";
                    else if (plataformaNombre.Contains("3do"))
                        imgPath = "logo_threedo";
                }

                return $"{imgPath}_dark.png";
            }
            set {}
        }
    }
}
