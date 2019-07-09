using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Db;
using GamesApi.Models.Game;
using GamesApi.Models.Report;

namespace GamesApi.Services
{
    public class GameRetrievalService : IGameRetrievalService
    {
        private readonly IMongoDbContext context;
        public GameRetrievalService(IMongoDbContext _context)
        {
            context = _context;
        }

        public AvgLikesReport GenerateAvgLikesReport(Game game)
        {
            throw new NotImplementedException();
        }

        public int GetAverageLikesOfGame(Game game)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get games where a comment has been added after the passed epoch time parameter
        /// </summary>
        /// <param name="games">collection of games</param>
        /// <param name="epochTimeAfter">unix timestamp in seconds</param>
        /// <returns></returns>
        public List<Game> GetByCommentsAfterEpoch(List<Game> games, long epochTimeAfter)
        {
            return games.Where(game => game.gameDetails.comments.
                Any(comment => Convert.ToInt64(comment.dateCreated) > epochTimeAfter)).
                ToList();
        }

        /// <summary>
        /// Get games that have a greater amount of comments than parameter
        /// </summary>
        /// <param name="games">Collection of games</param>
        /// <param name="minAmountOfComments">the minimum amount of comments a game must have</param>
        /// <returns></returns>
        public List<Game> GetByGreaterThanAmountOfComments(List<Game> games, int minAmountOfComments)
        {
            return games.Where(game => game.gameDetails.comments.Count() > minAmountOfComments).
            ToList();
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
        /// <summary>
        /// Retrieve games that have a lower age rating than the max age
        /// </summary>
        /// <param name="maxAgeRating">int age rating to filter by</param>
        /// <param name="games">Collection of games</param>
        /// <returns></returns>
        public List<Game> RetrieveByMaxAge(int maxAgeRating, List<Game> games)
        {
            return games.Where(game => Convert.ToInt32(game.gameDetails.age_rating) < maxAgeRating).
            ToList();
        }

        /// <summary>
        /// Retrieve games that a higher age rating than the passed in int parameter
        /// </summary>
        /// <param name="minAgeRating">int age rating to filter by</param>
        /// <param name="games">Collection of games</param>
        /// <returns></returns>
        public List<Game> RetrieveByMinAge(int minAgeRating, List<Game> games)
        {
            return games.Where(game => Convert.ToInt32(game.gameDetails.age_rating) >= minAgeRating).
            ToList();
        }

        /// <summary>
        /// Retrieve games that are on the platform(s) passed in the list parameter
        /// </summary>
        /// <param name="platforms">list of platforms to check for</param>
        /// <param name="games">collection of games, we must check to see whether the platform matches</param>
        /// <returns></returns>
        public List<Game> RetrieveByPlatforms(List<string> platforms, List<Game> games)
        {
            return games.Where(game => game.gameDetails.platform.Intersect(platforms, StringComparer.OrdinalIgnoreCase).
            Any()).
            ToList();
        }

        /// <summary>
        /// Retrieve games that were published by the publisher passed in as a string parameter
        /// </summary>
        /// <param name="publisher">publisher string parameter, will be checked against collection of games</param>
        /// <param name="games">collection of games</param>
        /// <returns></returns>
        public List<Game> RetrieveByPublisher(string publisher, List<Game> games)
        {
            return games.Where(game => game.gameDetails.by.Equals(publisher, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
        }
    }
}
