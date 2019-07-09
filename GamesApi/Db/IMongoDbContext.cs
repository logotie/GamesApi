using GamesApi.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Db
{
    //Interface for the MongoDb local database, allows me to moq the object later on
    public interface IMongoDbContext
    {
        //Retrieve all the games
        List<Game> Get();
        //Retrieve the game by id
        Game GetById(int id);
    }
}
