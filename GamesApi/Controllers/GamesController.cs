using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/games/5
        [HttpGet("{id:int}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

    }
}
