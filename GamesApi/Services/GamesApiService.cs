using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Db;
using GamesApi.Models.Game;
using GamesApi.Models.Report;
using GamesApi.Models.Url;

namespace GamesApi.Services
{
    /// <summary>
    /// Responsible for handling the requests of the Games controller
    /// </summary>
    public class GamesApiService : IGamesApiService
    {
        private readonly IMongoDbContext _mongoDbContext;
        private readonly GameRetrievalService gameRetrievalService;

        //Initialize with the MongoDbContext, we use an interface so it can be injected.
        public GamesApiService(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            gameRetrievalService = new GameRetrievalService(mongoDbContext);
        }

        public ReportComplete GenerateReport()
        {
            throw new NotImplementedException();
        }

        public Game RetrieveGameById(int id)
        {
            return gameRetrievalService.RetrieveById(id);
        }

        public List<Game> SearchGamesByQueryParams(GameQueryParams query)
        {
            throw new NotImplementedException();
        }
    }
}
