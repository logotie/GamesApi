using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Models.Game;
using GamesApi.Models.Report;
using GamesApi.Models.Url;
using GamesApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesApiService gamesApiService;

        public GamesController(IGamesApiService _gamesApiService)
        {
            gamesApiService = _gamesApiService;
        }

        // GET api/games/5
        [HttpGet("{id:int}")]
        public ActionResult<string> Get(int id)
        {
            //Retrieve the game by using the id parameter
            Game game = gamesApiService.RetrieveGameById(id);

            //If game does not exist return 404
            if (game == null)
            {
                return BadRequest();
            }

            //Return an indented json string to be displayed to the user
            String jsonString = JsonConvert.SerializeObject(game.gameDetails, Formatting.Indented);

            return jsonString;
        }

        /// <summary>
        /// Allows a user to search for a game using any of the params listed below
        /// </summary>
        /// <param name="queryParams">
        /// likesGreaterThan
        /// platform
        /// publisher
        /// publishedAfter
        /// minAgeRating
        /// maxAgeRating
        /// minCommentsAmount
        /// </param>
        /// The queries will be in the form of:
        /// api/games?minagerating=5&likesgreaterthan=4
        /// <returns></returns>
        // GET api/games?minagerating=5&likesgreaterthan=4
        [HttpGet()]
        public ActionResult<string> QuerySearch([FromQuery] GameQueryParams queryParams)
        {
            if (queryParams == null)
            {
                return BadRequest();
            }

            List<Game> games = gamesApiService.SearchGamesByQueryParams(queryParams);

            if (games == null)
            {
                return "";
            }

            List<GameDetails> gameDetails = new List<GameDetails>();

            games.ForEach(game => gameDetails.Add(game.gameDetails));

            string jsonString = JsonConvert.SerializeObject(gameDetails, Formatting.Indented);

            return jsonString;
        }

        [HttpGet("/games/report")]
        public ActionResult<string> GetReport()
        {
            ReportComplete report = gamesApiService.GenerateReport();

            String jsonReport = JsonConvert.SerializeObject(report, Formatting.Indented);

            return jsonReport;
        }

    }
}
