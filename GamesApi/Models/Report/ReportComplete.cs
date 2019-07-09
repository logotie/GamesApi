using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Models.Report
{
    public class ReportComplete
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }
        public string user_with_most_comments { get; set; }
        public string highest_rated_game { get; set; }
        public List<AvgLikesReport> average_likes_per_game { get; set; }
    }
}
