using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Helper;
using GamesApi.Models.Game;
using Mongo2Go;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace GamesApi.Db
{
    //Db context for local mongo db instance, handles data access operations
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IList<Game> _games;
        private MongoDbRunner _runner;

        //Going to use this for now, then I will add Mongo Db later
        public MongoDbContext()
        {
            _runner = MongoDbRunner.StartForDebugging();

            MongoClient client = new MongoClient(_runner.ConnectionString);
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("Games");
            var collection = database.GetCollection<Game>("GameItems");
            collection.WithWriteConcern(WriteConcern.Acknowledged);

            List<Game> games = RetrieveObjectsFromJson();

 
            foreach(Game game in games)
            {
                try
                {
                    collection.Insert<Game>(game);
                }
                catch(Exception e)
                {
                    //Should crash
                    //TODO decide how to to handle this
                }
            }

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
