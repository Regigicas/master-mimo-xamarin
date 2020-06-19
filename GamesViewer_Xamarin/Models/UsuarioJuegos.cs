using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GamesViewer_Xamarin.Models
{
    public class UsuarioJuegos
    {
        [ForeignKey(typeof(Usuario)), PrimaryKey]
        public int UsuarioId { get; set; }
        [ForeignKey(typeof(JuegoFav)), PrimaryKey]
        public int JuegoId { get; set; }
    }
}
