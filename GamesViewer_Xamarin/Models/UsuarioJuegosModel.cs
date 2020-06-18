using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GamesViewer_Xamarin.Models
{
    public class UsuarioJuegosModel
    {
        [ForeignKey(typeof(UsuarioModel)), PrimaryKey]
        public int UsuarioId { get; set; }
        [ForeignKey(typeof(JuegoFavModel)), PrimaryKey]
        public int JuegoId { get; set; }
    }
}
