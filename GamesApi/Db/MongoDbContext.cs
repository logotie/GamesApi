using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Helper;
using GamesApi.Models.Game;
using Newtonsoft.Json;

namespace GamesApi.Db
{
    //Db context for local mongo db instance, handles data access operations
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IList<Game> _games;

        //Going to use this for now, then I will add Mongo Db later
        public MongoDbContext()
        {
            _games = RetrieveObjectsFromJson();
        }

        public List<Game> Get()
        {
            return _games.ToList();
        }

        public Game GetById(int id)
        {
            return _games.FirstOrDefault(game => game.id == id);
        }

        private List<Game> RetrieveObjectsFromJson()
        {
            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(GamesApi.Resource.GamesArrayJson);
            DbHelper.FormatCommentDatesForUi(games);
            return games;
        }
    }
}
