using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GamesViewer_Xamarin.Models;
using Refit;

namespace GamesViewer_Xamarin.Interfaces
{
    public interface IJuegosRequest
    {
        [Get("/games")]
        Task<JuegoModel.ResponseQuery> GetJuegos(int page);
    }
}
