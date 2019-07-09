using GamesApi.Db;
using GamesApi.Models;
using GamesApi.Models.Game;
using GamesApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace GamesApiXunitTests
{
    public class GameRetrievalServiceTests
    {
        private readonly IGameRetrievalService gameRetrievalService2;
        private Mock<Comment> mockComment, mockComment2, mockComment3;
        private List<Comment> mockListComment, mockListComment2;
        private Mock<Game> mockGame, mockGame2;
        private Mock<IMongoDbContext> mockMongoDb;

        private readonly int GAMEID = 2;
        private readonly int GAMEID2 = 3;
        private readonly string PUBLISHERSONY = "song";
        private readonly string PUBLISHERMICROSOFT = "microsoft";
        private readonly string PUBLISHERTHQ = "thq";
        private readonly string PLATFORMPS4 = "ps4";
        private readonly string PLATFORMXBOXONE = "xboxone";
        public GameRetrievalServiceTests()
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
            mockComment3.
                Setup(s => s.like).
                Returns(15);

            mockListComment = new List<Comment>
            {
                mockComment.Object, mockComment2.Object
            };

            mockListComment2 = new List<Comment>
            {
                mockComment3.Object
            };

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

            mockGame2 = new Mock<Game>();
            mockGame2.
                Setup(s => s.gameDetails.comments).
                Returns(mockListComment2);
            mockGame2.
                SetupGet(s => s.id).
                Returns(GAMEID2);
            mockGame2.
                SetupGet(s => s.gameDetails.by).
                Returns(PUBLISHERSONY);

            mockMongoDb = new Mock<IMongoDbContext>();
            mockMongoDb.
                Setup(mockMongo => mockMongo.Get()).
                Returns(new List<Game> { mockGame.Object, mockGame2.Object });

            var mockGameRetrievalService = new Mock<IGameRetrievalService>();
            gameRetrievalService2 = new GameRetrievalService(mockMongoDb.Object);

        }


        [Fact]
        public void GenerateAverageLikesReport()
        {
            mockGame.SetupGet(game => game.gameDetails.title).Returns("lego star wars");

            var result = gameRetrievalService2.GenerateAvgLikesReport(mockGame.Object);
            int averageLikesOfGame = gameRetrievalService2.GetAverageLikesOfGame(mockGame.Object);
            Assert.True(result.title.Equals("lego star wars"));
            Assert.True(result.average_likes == averageLikesOfGame);
        }

        [Fact]
        public void GetAverageLikesOfGame()
        {
            int averageLikesOfGame = gameRetrievalService2.GetAverageLikesOfGame(mockGame.Object);
            Assert.True(averageLikesOfGame == 7);
        }

        [Fact]
        public void RetrieveById()
        {
            Game game = gameRetrievalService2.RetrieveById(GAMEID);
            Assert.True(game.id == GAMEID);
        }

        [Fact]
        public void RetrieveByHighestSumOfLikes()
        {
            Game game = gameRetrievalService2.RetrieveByHighestSumOfLikes();
            Assert.True(game.id == GAMEID2);
        }

        [Fact]
        public void RetrieveByPublisher()
        {
            List<Game> game = gameRetrievalService2.RetrieveByPublisher(PUBLISHERTHQ, mockMongoDb.Object.Get());
            Assert.True(game.Count == 1);
            Assert.True(game[0].id == GAMEID);
        }

        [Fact]
        public void RetrieveByPlatforms()
        {
            mockGame.
                SetupGet(s => s.gameDetails.platform).
                Returns(new List<string> { PLATFORMPS4 });

            mockGame2.
                SetupGet(s => s.gameDetails.platform).
                Returns(new List<string> { PLATFORMXBOXONE });

            List<Game> game = gameRetrievalService2.RetrieveByPlatforms(new List<string> { PLATFORMXBOXONE }, mockMongoDb.Object.Get());
            Assert.True(game.Count == 1);
            Assert.True(game[0].id == GAMEID2);
        }

        [Fact]
        public void RetrieveByLikesGreaterThan()
        {
            mockGame.
                SetupGet(s => s.gameDetails.likes).
                Returns(20);

            mockGame2.
                SetupGet(s => s.gameDetails.likes).
                Returns(55);

            List<Game> game = gameRetrievalService2.RetrieveByLikesGreaterThan(50, mockMongoDb.Object.Get());
            Assert.True(game.Count == 1);
            Assert.True(game[0].id == GAMEID2);
        }

        [Fact]
        public void RetrieveByMinAge()
        {
            mockGame.
                SetupGet(s => s.gameDetails.age_rating).
                Returns("15");

            mockGame2.
                SetupGet(s => s.gameDetails.age_rating).
                Returns("18");

            List<Game> game = gameRetrievalService2.RetrieveByMinAge(15, mockMongoDb.Object.Get());
            Assert.True(game.Count == 2);
            Assert.True(game[0].id == GAMEID);
            Assert.True(game[1].id == GAMEID2);
        }

        [Fact]
        public void RetrieveByMaxAge()
        {
            mockGame.
                SetupGet(s => s.gameDetails.age_rating).
                Returns("12");

            mockGame2.
                SetupGet(s => s.gameDetails.age_rating).
                Returns("18");

            List<Game> game = gameRetrievalService2.RetrieveByMaxAge(13, mockMongoDb.Object.Get());
            Assert.True(game.Count == 1);
            Assert.True(game[0].id == GAMEID);
        }

        [Fact]
        public void GetByGreaterThanAmountOfComments()
        {
            var games = gameRetrievalService2.GetByGreaterThanAmountOfComments(mockMongoDb.Object.Get(), 1);

            Assert.True(games.Count == 1);
            Assert.True(games[0].id == GAMEID);
        }

        [Fact]
        public void GetUserWithMostComments()
        {
            mockComment.
                SetupGet(comment => comment.user).
                Returns("Robert");

            mockComment2.
                SetupGet(comment => comment.user).
                Returns("Jack");

            mockComment3.
                SetupGet(comment => comment.user).
                Returns("Jack");

            string user = gameRetrievalService2.GetUserWithMostComments(mockMongoDb.Object.Get());
            Assert.True(user.Equals("Jack"));

        }

        [Fact]
        public void GetByCommentsAfterEpoch()
        {
            //Date 1
            string date = "20121004";
            DateTimeOffset dateTime1 = DateTimeOffset.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
            long epochTime = dateTime1.ToUnixTimeSeconds();

            //Date 2
            string date2 = "20151004";
            DateTimeOffset dateTime2 = DateTimeOffset.ParseExact(date2, "yyyyMMdd", CultureInfo.InvariantCulture);
            long epochTime2 = dateTime2.ToUnixTimeSeconds();

            //Date 2
            string date3 = "20161004";
            DateTimeOffset dateTime3 = DateTimeOffset.ParseExact(date3, "yyyyMMdd", CultureInfo.InvariantCulture);
            long epochTime3 = dateTime3.ToUnixTimeSeconds();

            mockComment.
                SetupGet(comment => comment.dateCreated).
                Returns(epochTime.ToString());

            mockComment2.
                SetupGet(comment => comment.dateCreated).
                Returns(epochTime2.ToString());

            mockComment3.
                SetupGet(comment => comment.dateCreated).
                Returns(epochTime3.ToString());

            var comments = gameRetrievalService2.GetByCommentsAfterEpoch(mockMongoDb.Object.Get(), epochTime2);
            Assert.True(comments.Count == 1);
            Assert.True(comments[0].id == GAMEID2);
        }

    }
}
