using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Db;
using GamesApi.Models.Game;
using GamesApi.Models.Url;

namespace GamesApi.Services
{
    /// <summary>
    /// Responsible for handling the requests of the Games controller
    /// </summary>
    public class GamesApiService : IGamesApiService
    {
        private readonly IMongoDbContext _mongoDbContext;

        //Initialize with the MongoDbContext, we use an interface so it can be injected.
        public GamesApiService(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public Game RetrieveGameById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Game> SearchGamesByQueryParams(GameQueryParams query)
        {
            throw new NotImplementedException();
        }
    }
}
