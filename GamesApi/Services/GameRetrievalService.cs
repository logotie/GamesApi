﻿using System;
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
        /// <summary>
        /// Retrieves the game that has the highest sum of likes from all comments
        /// </summary>
        /// <returns>The Game object with the highest sum of comment likes</returns>
        public Game RetrieveByHighestSumOfLikes()
        {
            //Stores found users and their likes count
            Dictionary<int, int> userAndLikesCount = new Dictionary<int, int>();

            //Go through each comment in each game, keeping a count of each user found and likes count
            foreach (Game game in context.Get())
            {
                foreach (Comment comment in game.gameDetails.comments)
                {
                    int gameId = game.id;

                    if (userAndLikesCount.ContainsKey(gameId))
                    {
                        userAndLikesCount[gameId] = userAndLikesCount[gameId] + 1;
                    }
                    else
                    {
                        userAndLikesCount.Add(gameId, 1);
                    }
                }
            }

            //Order the list by descending order
            int ordered = userAndLikesCount.OrderBy(x => x.Value).FirstOrDefault().Key;

            //Get the first result or return null if not found
            return context.Get().FirstOrDefault(x => x.id == ordered);
        }
        /// <summary>
        /// Retrieve the game that matches the specific id or return null if not found
        /// </summary>
        /// <param name="id">id of the game</param>
        /// <returns>Game object with the matching id</returns>
        public Game RetrieveById(int id)
        {
            return context.Get().FirstOrDefault(game => game.id == id);
        }
        /// <summary>
        /// Retrieves the game object where the 'like' field of the game details, is greater than the parameter integer
        /// </summary>
        /// <param name="minAmountOfLikes">Minimum amount of likes that a game must have</param>
        /// <param name="games">Games that a greater amount of likes than the parameter</param>
        /// <returns></returns>
        public List<Game> RetrieveByLikesGreaterThan(int minAmountOfLikes, List<Game> games)
        {
            return games.Where(game => game.gameDetails.likes >= minAmountOfLikes).ToList();
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
