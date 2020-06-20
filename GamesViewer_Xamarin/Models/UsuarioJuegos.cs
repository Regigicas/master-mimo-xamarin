using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GamesViewer_Xamarin.Models
{
    public class UsuarioJuegos
    {
        [ForeignKey(typeof(Usuario)), Indexed(Name = "CompositeKey", Order = 1, Unique = true)]
        public int UsuarioId { get; set; }
        [ForeignKey(typeof(JuegoFav)), Indexed(Name = "CompositeKey", Order = 2, Unique = true)]
        public int JuegoId { get; set; }
    }
}
