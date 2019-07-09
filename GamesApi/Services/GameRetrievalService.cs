using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Db;
using GamesApi.Models.Game;

namespace GamesApi.Services
{
    public class GameRetrievalService : IGameRetrievalService
    {
        private readonly IMongoDbContext context;
        public GameRetrievalService(IMongoDbContext _context)
        {
            context = _context;
        }

        public List<Game> GetByCommentsAfterEpoch(List<Game> games, long epochTimeAfter)
        {
            throw new NotImplementedException();
        }

        public List<Game> GetByGreaterThanAmountOfComments(List<Game> games, int minAmountOfComments)
        {
            throw new NotImplementedException();
        }

        public string GetUserWithMostComments(List<Game> games)
        {
            throw new NotImplementedException();
        }

        public Game RetrieveByHighestSumOfLikes()
        {
            throw new NotImplementedException();
        }

        public Game RetrieveById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Game> RetrieveByLikesGreaterThan(int minAmountOfLikes, List<Game> games)
        {
            throw new NotImplementedException();
        }

        public List<Game> RetrieveByMaxAge(int maxAgeRating, List<Game> games)
        {
            throw new NotImplementedException();
        }

        public List<Game> RetrieveByMinAge(int minAgeRating, List<Game> games)
        {
            throw new NotImplementedException();
        }

        public List<Game> RetrieveByPlatforms(List<string> platforms, List<Game> games)
        {
            throw new NotImplementedException();
        }

        public List<Game> RetrieveByPublisher(string publisher, List<Game> games)
        {
            throw new NotImplementedException();
        }
    }
}
