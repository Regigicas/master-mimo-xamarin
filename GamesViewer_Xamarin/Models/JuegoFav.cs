using System;
using SQLite;

namespace GamesViewer_Xamarin.Models
{
    public class JuegoFav
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundImage { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
