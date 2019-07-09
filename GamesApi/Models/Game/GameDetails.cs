using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Models.Game
{
    public class GameDetails
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }
        public virtual string title { get; set; }
        public virtual string description { get; set; }
        public virtual string by { get; set; }
        public virtual IList<string> platform { get; set; }
        public virtual string age_rating { get; set; }
        public virtual int likes { get; set; }
        public virtual IList<Comment> comments { get; set; }
    }
}
