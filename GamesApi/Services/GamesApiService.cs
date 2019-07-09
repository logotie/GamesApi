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

        //Generates a complete report using the list of average report.
        public ReportComplete GenerateReport()
        {
            ReportComplete report = new ReportComplete();
            report.highest_rated_game = gameRetrievalService.RetrieveByHighestSumOfLikes().gameDetails.title;
            report.user_with_most_comments = gameRetrievalService.GetUserWithMostComments(_mongoDbContext.Get());
            report.average_likes_per_game = GenerateAllAvgReport();
            return report;
        }

        //Generates an average report for all the games.
        private List<AvgLikesReport> GenerateAllAvgReport()
        {
            List<AvgLikesReport> avgReports = new List<AvgLikesReport>();

            foreach (Game game in _mongoDbContext.Get())
            {
                AvgLikesReport avgReport = gameRetrievalService.GenerateAvgLikesReport(game);
                avgReports.Add(avgReport);
            }

            return avgReports;
        }

        /// <summary>
        /// Retrieves a game by the id of the game.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Game RetrieveGameById(int id)
        {
            return gameRetrievalService.RetrieveById(id);
        }

        /// <summary>
        /// Search games by query parameters passed in from the url
        /// The params are converted into a model and used to search
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Game> SearchGamesByQueryParams(GameQueryParams query)
        {
            //Likes greater than search
            var results = gameRetrievalService.RetrieveByLikesGreaterThan(query.likesGreaterThan, _mongoDbContext.Get());

            //Publisher search
            if (query.publisher != null)
                results = gameRetrievalService.RetrieveByPublisher(query.publisher, results);

            //age rating max age search
            if (query.maxAgeRating != null)
                results = gameRetrievalService.RetrieveByMaxAge(query.maxAgeRating.Value, results);

            //Platform search
            if (query.platform != null)
                results = gameRetrievalService.RetrieveByPlatforms(query.platform, results);

            //Comments published after search
            if (query.publishedAfter != null)
                results = gameRetrievalService.GetByCommentsAfterEpoch(results, Convert.ToInt64(query.publishedAfter));

            //age rating range minimum search
            results = gameRetrievalService.RetrieveByMinAge(query.minAgeRating, results);

            //amount of comments search
            results = gameRetrievalService.GetByGreaterThanAmountOfComments(results, query.minCommentsAmount);

            return results;
        }
    }
}
