using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Models.Game;
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
            Game game = gamesApiService.RetrieveGameById(id);

            if (game == null)
            {
                return BadRequest();
            }

            String jsonString = JsonConvert.SerializeObject(game.gameDetails, Formatting.Indented);

            return jsonString;
        }

    }
}
