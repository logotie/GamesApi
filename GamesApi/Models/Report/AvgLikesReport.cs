using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Models.Report
{
    public class AvgLikesReport
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }
        public string title { get; set; }
        public int average_likes { get; set; }
    }
}
