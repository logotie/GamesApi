using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Helper;
using GamesApi.Models.Game;
using Mongo2Go;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;

namespace GamesApi.Db
{
    //Db context for local mongo db instance, handles data access operations
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IQueryable<Game> _games;
        private MongoDbRunner _runner;

        //Mongo Db local instance 
        public MongoDbContext()
        {
            _runner = MongoDbRunner.StartForDebugging();

            MongoClient client = new MongoClient(_runner.ConnectionString);
            //TODO Change this
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("Games");

            //Get the collection for writing
            var collection = database.GetCollection<Game>("GameItems");

            //We want to know that it has been written successfully
            collection.WithWriteConcern(WriteConcern.Acknowledged);

            //Retrieve the game objects from the json
            List<Game> games = RetrieveObjectsFromJson();

            foreach(Game game in games)
            {
                try
                {
                    //Attempt to add each object to the mango db
                    collection.Insert<Game>(game);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unable to write to MongoDb, game Id:" + game.id);
                }
            }

            //IQueryable uses lazy loading so it is not actually reading the entire list to memory
            //It will only actually load the data when 'tolist' or the query is explicitly executed
            _games = collection.AsQueryable<Game>();
        }

        //Converts the queryable to a list therefore the queryable will be converted from 
        //an expression tree to the game objects in memory
        public List<Game> Get()
        {
            return _games.ToList();
        }

        //Returns the game id 
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
