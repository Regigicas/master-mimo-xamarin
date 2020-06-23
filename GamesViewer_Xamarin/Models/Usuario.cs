using System;
using SQLite;

namespace GamesViewer_Xamarin.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Username { get; set; }
        [Unique]
        public string Email { get; set; }
        public string ShaHashPass { get; set; }

        [Ignore]
        public string UsernameEmail
        {
            get
            {
                return $"{Username} - {Email}";
            }
            set {}
        }
    }
}
