using GamesApi.Db;
using GamesApi.Models.Game;
using GamesApi.Models.Report;
using GamesApi.Models.Url;
using GamesApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GamesApiXunitTests
{
    public class GamesApiServiceTests
    {
        //Readonly because it shouldn't change
        private readonly IGamesApiService gamesApiService;

        //We use Mock to quickly setup the object
        private Mock<IMongoDbContext> mockMongoDb;
        private Mock<Comment> mockComment, mockComment2, mockComment3;
        private readonly List<Comment> mockListComment, mockListComment2;
        private Mock<Game> mockGame, mockGame2;

        private readonly int GAMEID = 2;
        private readonly int GAMEID2 = 3;

        private readonly string PUBLISHERSONY = "SONY";
        private readonly string PUBLISHERMICROSOFT = "microsoft";
        private readonly string PUBLISHERTHQ = "thq";
        private readonly string PLATFORMPS4 = "ps4";
        private readonly string PLATFORMXBOXONE = "xboxone";

        public GamesApiServiceTests()
        {
            mockComment = new Mock<Comment>();
            mockComment.
                Setup(s => s.like).
                Returns(5);

            mockComment2 = new Mock<Comment>();
            mockComment2.
                Setup(s => s.like).
                Returns(9);

            mockComment3 = new Mock<Comment>();

            mockListComment = new List<Comment>
            {
                mockComment.Object, mockComment2.Object
            };

            mockListComment2 = new List<Comment>
            {
                mockComment3.Object
            };

            mockComment3.
                Setup(s => s.like).
                Returns(200);

            mockComment.
                SetupGet(comment => comment.user).
                Returns("Robert");

            mockComment2.
                SetupGet(comment => comment.user).
                Returns("Jack");

            mockComment3.
                SetupGet(comment => comment.user).
                Returns("Jack");

            mockGame = new Mock<Game>();
            mockGame.
                Setup(s => s.gameDetails.comments).
                Returns(mockListComment);
            mockGame.
                SetupGet(s => s.id).
                Returns(GAMEID);
            mockGame.
                SetupGet(s => s.gameDetails.by).
                Returns(PUBLISHERTHQ);

            mockGame.
                SetupGet(s => s.gameDetails.age_rating).
                Returns("12");

            mockGame2 = new Mock<Game>();
            mockGame2.
                Setup(s => s.gameDetails.comments).
                Returns(mockListComment2);
            mockGame2.
                Setup(s => s.gameDetails.title).
                Returns("TESTGAME3");
            mockGame2.
                SetupGet(s => s.id).
                Returns(GAMEID2);
            mockGame2.
                SetupGet(s => s.gameDetails.by).
                Returns(PUBLISHERSONY);
            mockGame2.
                SetupGet(s => s.gameDetails.platform).
                Returns(new List<string> { "XBOX ONE" });
            mockGame2.
                SetupGet(s => s.gameDetails.age_rating).
                Returns("15");

            mockMongoDb = new Mock<IMongoDbContext>();
            mockMongoDb.
                Setup(mockMongo => mockMongo.Get()).
                Returns(new List<Game> { mockGame.Object, mockGame2.Object });

            var mockGameRetrievalService = new Mock<IGameRetrievalService>();
            gamesApiService = new GamesApiService(mockMongoDb.Object);

        }

        [Fact]
        public void RetrieveGameByIdTest()
        {
            Game game = gamesApiService.RetrieveGameById(GAMEID);
            Assert.True(game.id == GAMEID);
        }

        [Fact]
        public void SearchGamesTest()
        {
            var queryModel = new Mock<GameQueryParams>();
            queryModel.
                Setup(s => s.publisher).
                Returns("SONY");
            queryModel.
                Setup(s => s.minAge).
                Returns(0);
            queryModel.
                Setup(s => s.maxAge).
                Returns(16);
            queryModel.
                Setup(s => s.minComments).
                Returns(0);
            queryModel.
                Setup(s => s.platform).
                Returns(new List<string> { "XBOX ONE" });


            List<Game> games = gamesApiService.SearchGamesByQueryParams(queryModel.Object);
            Assert.True(games.Count == 1);
            Assert.True(games[0].id == GAMEID2);

        }

        [Fact]
        public void GenerateReportTest()
        {
            ReportComplete report = gamesApiService.GenerateReport();
            Assert.True(report.highest_rated_game.Equals("TESTGAME3"));
            Assert.True(report.user_with_most_comments.Equals("Jack"));
        }
    }
}
