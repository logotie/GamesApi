using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Models.Game
{
    public class Game
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }
        public virtual int id { get; set; }
        [JsonProperty("Value")]
        public virtual GameDetails gameDetails { get; set; }
    }
}
