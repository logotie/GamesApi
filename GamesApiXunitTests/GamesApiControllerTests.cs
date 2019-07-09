using GamesApi.Controllers;
using GamesApi.Helper;
using GamesApi.Models.Game;
using GamesApi.Services;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace GamesApiXunitTests
{
    public class GamesApiControllerTests
    {
        [Fact]
        public void GetByIdApiTest()
        {
            int pageId = 1;

            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(GamesApi.Resource.GamesArrayJson);

            DbHelper.FormatCommentDatesForUi(games);

            var searchService = new Mock<IGamesApiService>();
            searchService.Setup(s => s.RetrieveGameById(1)).
                Returns(games[0]);

            var controller = new GamesController(searchService.Object);

            var result = controller.Get(pageId);

            Assert.True(result.Value.Equals(GamesApi.Resource.GamesArrayJson));
        }
    }
}
