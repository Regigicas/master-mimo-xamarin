using System.Collections.Generic;
using Newtonsoft.Json;

namespace GamesViewer_Xamarin.Models
{
    public class Juego
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Released { get; set; }
        [JsonProperty(PropertyName = "background_image")]
        public string BackgroundImage { get; set; }
        public float Rating { get; set; }
        [JsonProperty(PropertyName = "platforms")]
        private List<Platform.PlatformsResponse> Platforms { get; set; }
        [JsonIgnore]
        public string DescriptionWithPlaceholder
        {
            get
            {
                return string.IsNullOrEmpty(Description) ? Resx.AppResources.NoDescription : Description;
            }
            set { }
        }

        [JsonIgnore]
        public List<Platform> ParsedPlatforms
        {
            get
            {
                var values = new List<Platform>();
                if (Platforms != null)
                    foreach (var value in Platforms)
                        values.Add(value.Platform);

                return values;
            }
            set {}
        }

        public class ResponseQuery
        {
            public List<Juego> Results { get; set; }
        }

        public string BackgroundString
        {
            get
            {
                if (string.IsNullOrEmpty(BackgroundImage))
                    return "placeholder_image";

                var splits = BackgroundImage.Split('/');
                var url1 = splits[splits.Length - 1];
                var url2 = splits[splits.Length - 2];
                var url3 = splits[splits.Length - 3];
                var backgroundUrl = $"https://api.rawg.io/media/crop/600/400/{url3}/{url2}/{url1}";
                return backgroundUrl;
            }
            set {}
        }

        public string PlatformString
        {
            get
            {
                var platforms = "Ninguna";
                var first = true;
                if (Platforms != null)
                {
                    foreach (var plat in ParsedPlatforms)
                    {
                        if (first)
                        {
                            first = false;
                            platforms = plat.Name;
                        }
                        else
                            platforms += $" | {plat.Name}";
                    }
                }

                return platforms;
            }
            set {}
        }
    }
}
