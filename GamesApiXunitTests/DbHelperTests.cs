﻿using GamesApi.Db;
using GamesApi.Helper;
using GamesApi.Models.Game;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GamesApiXunitTests
{
    public class DbHelperTests
    {
        //We use Mock to quickly setup the object
        private Mock<IMongoDbContext> mockMongoDb;
        private Mock<Comment> mockComment, mockComment2, mockComment3;
        private readonly List<Comment> mockListComment, mockListComment2;
        private Mock<Game> mockGame, mockGame2;

        private readonly int GAMEID = 2;
        private readonly int GAMEID2 = 3;

        public DbHelperTests()
        {
            mockComment = new Mock<Comment>();
            mockComment2 = new Mock<Comment>();
            mockComment3 = new Mock<Comment>();

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
            mockGame2 = new Mock<Game>();
            mockGame2.
                Setup(s => s.gameDetails.comments).
                Returns(mockListComment2);

            mockMongoDb = new Mock<IMongoDbContext>();
            mockMongoDb.
                Setup(mockMongo => mockMongo.Get()).
                Returns(new List<Game> { mockGame.Object, mockGame2.Object });

        }

        [Fact]
        public void ConvertEpochToDateTest()
        {
            long epoch = 1562680316;
            string epochDateRepresentation = "2019-07-09";

            var comment = new Comment();
            comment.dateCreated = epoch.ToString();

            var game = new Game();
            var gameDetails = new GameDetails();
            game.gameDetails = gameDetails;
            game.gameDetails.comments = new List<Comment> { comment };

            DbHelper.FormatCommentDatesForUi(new List<Game> { game });

            Assert.True(game.gameDetails.comments[0].dateCreated.Equals("2019-07-09"));

        }

    }


}
