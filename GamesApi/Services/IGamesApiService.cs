using GamesApi.Models.Game;
using GamesApi.Models.Report;
using GamesApi.Models.Url;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Services
{
    public interface IGamesApiService
    {
        List<Game> SearchGamesByQueryParams(GameQueryParams query);
        Game RetrieveGameById(int id);
        ReportComplete GenerateReport();
    }
}
