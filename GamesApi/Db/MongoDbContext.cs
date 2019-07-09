using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Models.Game;

namespace GamesApi.Db
{
    //Db context for local mongo db instance, handles data access operations
    public class MongoDbContext : IMongoDbContext
    {
        public List<Game> Get()
        {
            throw new NotImplementedException();
        }

        public Game GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
