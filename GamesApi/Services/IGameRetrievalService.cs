using GamesApi.Models.Game;
using GamesApi.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Services
{
    public interface IGameRetrievalService
    {
        Game RetrieveById(int id);
        Game RetrieveByHighestSumOfLikes();
        List<Game> RetrieveByPublisher(string publisher, List<Game> games);
        List<Game> RetrieveByPlatforms(List<String> platforms, List<Game> games);
        List<Game> RetrieveByLikesGreaterThan(int minAmountOfLikes, List<Game> games);
        List<Game> RetrieveByMinAge(int minAgeRating, List<Game> games);
        List<Game> RetrieveByMaxAge(int maxAgeRating, List<Game> games);
        int GetAverageLikesOfGame(Game game);
        AvgLikesReport GenerateAvgLikesReport(Game game);
        List<Game> GetByGreaterThanAmountOfComments(List<Game> games, int minAmountOfComments);
        String GetUserWithMostComments(List<Game> games);
        List<Game> GetByCommentsAfterEpoch(List<Game> games, long epochTimeAfter);

    }
}
